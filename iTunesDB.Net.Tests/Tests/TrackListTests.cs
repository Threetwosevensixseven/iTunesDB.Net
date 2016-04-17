using System;
using System.Diagnostics;
using System.Linq;
using iTunesDB.Net.Database;
using iTunesDB.Net.Enumerations;
using iTunesDB.Net.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesDB.Net.Tests
{
    [TestClass]
    public class TrackListTests : TestBase
    {
        [TestMethod, TestCategory("TrackList")]
        public void TrackList_TrackCount()
        {
            var rdrListContainer = Reader.Children.FirstOrDefault(r => ((ListContainer)(r.DbObject)).ListType == ListTypes.Tracks);
            var rdrTrackList = rdrListContainer.Children.FirstOrDefault();
            Assert.AreEqual(rdrTrackList.TotalSize, Db.Tracks.Count);
        }
    }
}
