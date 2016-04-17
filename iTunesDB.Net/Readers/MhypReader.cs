using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;

namespace iTunesDB.Net.Readers
{
    internal class MhypReader : iTunesReader
    {
        public override string ObjectID { get { return "mhyp"; } }
        public override string[] ChildIDs { get { return new string[] { "mhod", "mhip" }; } }
        public override Type DatabaseType { get { return typeof(PlayListItem); } }

        protected override void ParseiTunesObject(BinaryReader Reader)
        {
            var playListItem = (PlayListItem)DbObject;
            var playList = (PlayList)ParentDbObject;
            playList.Add(playListItem);
        }
    }
}