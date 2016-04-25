using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;
using iTunesDB.Net.Enumerations;

namespace iTunesDB.Net.Readers
{
    internal class MhltReader : iTunesReader
    {
        public override string ObjectID { get { return "mhlt"; } }
        public override string[] ChildIDs { get { return new string[] { "mhit" }; } }
        public override Type DatabaseType { get { return typeof(TrackList); } }
        protected override bool WhileCondition { get { return TotalSize > Children.Count; } }

        protected override bool ParseiTunesObject(BinaryReader Reader)
        {
            var trackList = (DbList)DbObject;
            var listContainer = (ListContainer)ParentDbObject;
            var db = (iTunesDb)GrandparentDbObject;

            if (listContainer.ListType != ListTypes.Tracks)
                throw new InvalidOperationException("Invalid ListType " 
                    + listContainer.ListType.ToString() + " for TrackList container");
            db.Tracks = (TrackList)trackList;
            listContainer.Add(trackList);
            return true;
        }
    }
}

