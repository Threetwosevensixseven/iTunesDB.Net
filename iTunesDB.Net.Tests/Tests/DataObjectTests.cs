using System;
using System.Diagnostics;
using System.Linq;
using iTunesDB.Net.Database;
using iTunesDB.Net.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesDB.Net.Tests
{
    [TestClass]
    public class DataObjectTests : TestBase
    {
        //[TestMethod, TestCategory("DataObject")]
        //public void DataObject_TypeDistribution()
        //{
        //    var tdobjs = MhodReader.TrackTypes;
        //    var plidobjs = MhodReader.PlayListItemTypes;
        //    var mhipdobjs = MhodReader.MhipObjectTypes;
        //    var t = "Track mhods=" + string.Join(",", tdobjs.Distinct().OrderBy(n => n));
        //    var pli = "PlaylistItem mhods=" + string.Join(",", plidobjs.Distinct().OrderBy(n => n));
        //    var mhip = "MhipItem mhods=" + string.Join(",", mhipdobjs.Distinct().OrderBy(n => n));
        //    var x = t + "\r\n" + pli + "\r\n" + mhip;
        //    Assert.AreEqual(-1, tdobjs.Count);
        //    Assert.AreEqual(-1, plidobjs.Count);
        //    Assert.AreEqual(-1, mhipdobjs.Count);
        //}
    }
}
