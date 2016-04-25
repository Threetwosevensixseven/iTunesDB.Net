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
    internal abstract class DataObjectReader : MhodReader
    {
        public abstract DO[] DataObjectTypes { get; }
        public override int ObjectSize { get { return ParentReader.ObjectSize; } set { ParentReader.ObjectSize = value; } }
        public override int TotalSize { get { return ParentReader.TotalSize; } set { ParentReader.TotalSize = value; } }
        public override int BytesRead { get { return ParentReader.BytesRead; } set { ParentReader.BytesRead = value; } }
        public override List<iTunesReader> Children { get { return ParentReader.Children; } set { ParentReader.Children = value; } }
        public override IDbObject DbObject { get { return ParentReader.DbObject; } set { ParentReader.DbObject = value; } }
        public override IDbObject ParentDbObject { get { return ParentReader.ParentDbObject; } }
        public override IDbObject GrandparentDbObject { get { return ParentReader.GrandparentDbObject; } }
        public abstract void ParseDataObject(BinaryReader Reader);

        public void SetDataObjectString(BinaryReader Reader, int Length)
        {
            var dotype = ((DataObject)DbObject).Type;
            var text = ReadStringUtfDetect(Reader, Length);
            foreach (var pi in ParentDbObject.GetType().GetProperties())
            {
                var attr = (DataObjectAttribute)pi.GetCustomAttributes(typeof(DataObjectAttribute), false)
                    .FirstOrDefault(a => ((DataObjectAttribute)a).Value.Equals(dotype.ToString(), StringComparison.InvariantCultureIgnoreCase));
                if (attr == null) continue;
                pi.SetValue(ParentDbObject, text, null);
                SetAdditionalDataObject(ParentDbObject, pi.Name, text);
                return;
            }
            foreach (var pi in ParentDbObject.GetType().GetProperties())
            {
                if (!pi.Name.Equals(dotype.ToString(), StringComparison.InvariantCultureIgnoreCase)) continue;
                pi.SetValue(ParentDbObject, text, null);
                SetAdditionalDataObject(ParentDbObject, pi.Name, text);
                return;
            }

            throw new InvalidOperationException(ParentDbObject.GetType().Name 
                + " does not have a property matching the " + dotype.ToString() + " object type.");
        }

        private void SetAdditionalDataObject(IDbObject Object, string Name, string Value)
        {
            if (Name == "Location")
            {
                var pi = ParentDbObject.GetType().GetProperty("LocationWindows");
                if (pi == null) return;
                if (Value == null) return;
                string val = Value.Replace(':', Path.DirectorySeparatorChar);
                var dbrdr = (MhbdReader)ParentReader.ParentReader.ParentReader.ParentReader.ParentReader;
                var db = (iTunesDb)dbrdr.DbObject;
                int pos = db.FileName.IndexOf(@"\iPod_Control\");
                if (pos < 0) return;
                string prefix = db.FileName.Substring(0, pos);
                string loc = prefix + val;
                pi.SetValue(Object, loc, null);
            }
        }
    }
}
