using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;

namespace iTunesDB.Net.Readers
{
    internal class MhbdReader : iTunesReader
    {
        public override string ObjectID { get { return "mhbd"; } }
        public override string[] ChildIDs { get { return new string[] { "mhsd" }; } }
        public override Type DatabaseType { get { return typeof(iTunesDb); } }
        private string fileName;
        public iTunesDb Db { get { return (iTunesDb)DbObject; } }

        public static MhbdReader Open(string FileName)
        {
            if (!File.Exists(FileName)) 
                throw new FileNotFoundException("iTunesDb file not found.", FileName);
            var db = new MhbdReader();
            db.fileName = FileName;
            using (var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new BinaryReader(fs))
            {
                string cname = Encoding.ASCII.GetString(reader.ReadBytes(4));
                db.Parse(reader);
                return db;
            }
        }

        protected override void ParseiTunesObject(BinaryReader Reader)
        {
            Db.FileName = fileName;
            Db.FileSize = TotalSize;
            var major = ReadInt32(Reader);
            var minor = ReadInt32(Reader);
            var patch = ReadInt32(Reader);
            Db.Version = new Version(major, minor, patch);
            Db.UnknownBytes = ReadRemainingBytes(Reader);
            Db.Tracks = new TrackList();
            Db.PlayLists = new PlayLists();
            Db.ListContainers = new List<ListContainer>();
        }
    }
}
