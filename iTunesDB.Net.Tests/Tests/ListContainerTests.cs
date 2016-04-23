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
    public class ListContainerTests : TestBase
    {
        [TestMethod, TestCategory("iTunesDb")]
        public void ListContainer_ListTypes()
        {
            int trackListContainers = Reader.Children.Count(c => ((ListContainer)c.DbObject).ListType == ListTypes.Tracks);
            int playListContainers = Reader.Children.Count(c => ((ListContainer)c.DbObject).ListType == ListTypes.PlayList);
            Assert.AreEqual(1, trackListContainers);
            Assert.AreEqual(1, playListContainers);
        }

        [TestMethod, TestCategory("iTunesDb")]
        public void ListContainer_Containers()
        {
            var x = Db.ListContainers;
        }

    }
}
