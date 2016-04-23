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
    public class PlayListsTests : TestBase
    {
        [TestMethod, TestCategory("iTunesDb")]
        public void PlayLists_PlayListCount()
        {
            var rdrPLContainer = Reader.Children.FirstOrDefault(r => ((ListContainer)(r.DbObject)).ListType == ListTypes.PlayList);
            var rdrSPPLContainer = Reader.Children.FirstOrDefault(r => ((ListContainer)(r.DbObject)).ListType == ListTypes.SpecialPodcastPlayList);
            var rdrPLContainerPlayLists = rdrPLContainer.Children.FirstOrDefault();
            var rdrSPPLContainerPlayLists = rdrSPPLContainer.Children.FirstOrDefault();
            Assert.AreEqual(rdrPLContainerPlayLists.TotalSize, Db.PlayLists.Count);
            Assert.AreEqual(rdrSPPLContainerPlayLists.TotalSize, Db.PlayLists.Count);
        }
    }
}
