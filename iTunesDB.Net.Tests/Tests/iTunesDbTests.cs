using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesDB.Net.Tests
{
    [TestClass]
    public class iTunesDbTests : TestBase
    {
        [TestMethod, TestCategory("iTunesDb")]
        public void iTunesDb_FileName()
        {
            Assert.AreEqual(DbFileName, Db.FileName);
        }

        [TestMethod, TestCategory("iTunesDb")]
        public void iTunesDb_FileSize()
        {
            Assert.AreEqual(20747208, Db.FileSize);
            Assert.AreEqual(DbFileSize, Db.FileSize);
        }

        [TestMethod, TestCategory("iTunesDb")]
        public void iTunesDb_Version()
        {
            Assert.AreEqual(new Version(1, 18, 3), Db.Version);
        }

        [TestMethod, TestCategory("iTunesDb")]
        public void iTunesDb_UnknownBytes()
        {
            Assert.AreEqual(80, Db.UnknownBytes.Length);
        }
    }
}
