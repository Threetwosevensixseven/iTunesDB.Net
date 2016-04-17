using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;

namespace iTunesDB.Net.Readers
{
    internal class MhlpReader : iTunesReader
    {
        public override string ObjectID { get { return "mhlp"; } }
        public override string[] ChildIDs { get { return new string[] { "mhyp" }; } }
        public override Type DatabaseType { get { return typeof(PlayList); } }
        protected override bool WhileCondition { get { return TotalSize > Children.Count; } }
    }
}

