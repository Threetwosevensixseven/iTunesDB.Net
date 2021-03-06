﻿using System;
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
        public override Type DatabaseType { get { return typeof(PlayList); } }

        protected override bool ParseiTunesObject(BinaryReader Reader)
        {
            var playList = (PlayList)DbObject;
            var playLists = (PlayLists)ParentDbObject;
            playLists.Add(playList);
            return true;
        }
    }
}