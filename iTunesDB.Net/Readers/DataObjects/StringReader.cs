using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Attributes;
using iTunesDB.Net.Database;
using DO = iTunesDB.Net.Enumerations.DataObjects; 

namespace iTunesDB.Net.Readers.DataObjects
{
    internal class StringReader : DataObjectReader
    {
        private static DO[] dataObjectTypes = new DO[] { DO.Title, DO.Location, DO.Album, DO.Artist, DO.Genre, 
            DO.FileType, DO.EQSetting, DO.Comment, DO.Category, DO.Composer, DO.Grouping, DO.Description };
        public override DO[] DataObjectTypes { get { return dataObjectTypes; } }

        public override void ParseDataObject(BinaryReader Reader)
        {
            var dotype = ((DataObject)DbObject).Type;          
            var obj = ParentDbObject;

            var unk1 = ReadInt32(Reader);
            var unk2 = ReadInt32(Reader);
            var position = ReadInt32(Reader);
            var length = ReadInt32(Reader);
            var unk3 = ReadInt32(Reader);
            var unk4 = ReadInt32(Reader);
            SetDataObjectString(Reader, length);
        }
    }
}
