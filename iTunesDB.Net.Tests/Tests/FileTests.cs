using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesDB.Net.Tests
{
    [TestClass]
    public class FileTests : TestBase
    {
        [TestMethod, TestCategory("Files")]
        public void ValidDbCanBeOpened()
        {
            Assert.IsNotNull(Db);
        }

        [TestMethod, TestCategory("Files")]
        [ExpectedException(typeof(FileNotFoundException))]
        public void InvalidDbCannotBeOpened()
        {
            MhbdReader.Open("C:\\INVALID_DIRECTORY\\INVALID_FILE");
        }
    }
}
