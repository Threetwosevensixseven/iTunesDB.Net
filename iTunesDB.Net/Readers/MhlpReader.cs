using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;
using iTunesDB.Net.Enumerations;

namespace iTunesDB.Net.Readers
{
    internal class MhlpReader : iTunesReader
    {
        public override string ObjectID { get { return "mhlp"; } }
        public override string[] ChildIDs { get { return new string[] { "mhyp" }; } }
        public override Type DatabaseType { get { return typeof(PlayLists); } }
        protected override bool WhileCondition { get { return TotalSize > Children.Count; } }
        public static readonly ListTypes[] AllowedLists = new ListTypes[] { ListTypes.PlayList, ListTypes.SpecialPodcastPlayList };

        protected override void ParseiTunesObject(BinaryReader Reader)
        {
            var playLists = (DbList)DbObject;
            var listContainer = (ListContainer)ParentDbObject;
            var db = (iTunesDb)GrandparentDbObject;

            if (!AllowedLists.Any(l => l == listContainer.ListType))
                throw new InvalidOperationException("Invalid ListType "
                    + listContainer.ListType.ToString() + " for TrackList container");
            listContainer.Add(playLists);
        }
    }
}

