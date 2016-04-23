using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;
using iTunesDB.Net.Enumerations;
using DO = iTunesDB.Net.Enumerations.DataObjects; 

namespace iTunesDB.Net.Readers
{
    internal class MhodReader : iTunesReader
    {
        public override string ObjectID { get { return "mhod"; } }
        public override string[] ChildIDs { get { return new string[0]; } }
        public override Type DatabaseType { get { return typeof(DataObject); } }
        public static List<DO> TrackTypes;
        public static List<DO> PlayListTypes;
        public static List<DO> MhipObjectTypes;

        protected override void ParseiTunesObject(BinaryReader Reader)
        {
            ObjectSize = TotalSize;
            var dobj = (DataObject)DbObject;
            dobj.Type = ReadEnum<DO>(Reader);
            var rdr = CreateDataObjectReader(dobj.Type, this);
            rdr.ParseDataObject(Reader);
            
            if (ParentDbObject is Track)
            {
                var track = (Track)ParentDbObject;
                TrackTypes.Add(dobj.Type);
            }
            else if (ParentDbObject is PlayList)
            {
                var playList = (PlayList)ParentDbObject;
                PlayListTypes.Add(dobj.Type);
            }
            else if (ParentDbObject is MhipObject)
            {
                var mhipobj = (MhipObject)ParentDbObject;
                MhipObjectTypes.Add(dobj.Type);
            }
            else throw new Exception("Unknown mhod parent type");
        }
    }
}

