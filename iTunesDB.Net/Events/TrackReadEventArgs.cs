using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;

namespace iTunesDB.Net.Events
{
    public class TrackReadEventArgs : EventArgs
    {
        public int TotalTracks { get; private set; }
        public int TracksRead { get; private set; }
        public bool Complete { get { return TracksRead >= TotalTracks; } }
        public bool Cancel { get; set; }
        public Track Track { get; private set; }

        public TrackReadEventArgs(int TotalTracks, int TracksRead, Track Track)
        {
            this.TotalTracks = TotalTracks;
            this.TracksRead = TracksRead;
            this.Track = Track;
        }
    }
}
