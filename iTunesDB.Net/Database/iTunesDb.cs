using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTunesDB.Net.Database
{
    public class iTunesDb : IDbObject
    {
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public Version Version { get; set; }
        public byte[] UnknownBytes { get; set; }
        public TrackList Tracks { get; set; }
        public PlayLists PlayLists { get; set; }
        public List<ListContainer> ListContainers { get; set; }
    }
}
