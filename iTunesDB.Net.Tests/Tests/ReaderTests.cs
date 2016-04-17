using System;
using System.Linq;
using iTunesDB.Net.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesDB.Net.Tests
{
    [TestClass]
    public class ReaderTests : TestBase
    {
        [TestMethod, TestCategory("Reader")]
        public void CanCreateKnownObjectID()
        {
            var obj = iTunesReader.CreateReader("mhbd", null);
            Assert.AreEqual("mhbd", obj.ObjectID);
            Assert.AreEqual(typeof(MhbdReader), obj.GetType());
        }

        [TestMethod, TestCategory("Reader")]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotCreateUnknownObjectID()
        {
            iTunesReader.CreateReader("xxxx", null);
        }

        [TestMethod, TestCategory("Reader")]
        public void CanOpenAndParse()
        {
            Assert.AreEqual(229824, Reader.AllChildren.Count());
        }
    }
}
