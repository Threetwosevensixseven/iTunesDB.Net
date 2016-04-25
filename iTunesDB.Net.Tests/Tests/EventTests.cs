using System;
using System.Collections.Generic;
using System.Linq;
using iTunesDB.Net.Database;
using iTunesDB.Net.Events;
using iTunesDB.Net.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesDB.Net.Tests
{
    [TestClass]
    public class EventTests
    {


        [TestMethod, TestCategory("Events")]
        public void Events_TrackRead()
        {
            #if ASYNC
            var rdr = new MhbdReader();
            var tracks = new List<Track>();
            rdr.TrackRead += (object sender, TrackReadEventArgs e) =>
            {
                tracks.Add(e.Track);
                e.Cancel = e.Complete;
            };
            rdr.Open(TestBase.DbFilePath + "iTunesDB");
            Assert.AreEqual(rdr.Db.Tracks.Count, tracks.Count);
            #endif
        }
    }
}
