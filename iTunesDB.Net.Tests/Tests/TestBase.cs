using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;
using iTunesDB.Net.Enumerations;
using iTunesDB.Net.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesDB.Net.Tests
{
    [TestClass]
    public class TestBase
    {
        //public const string PATH = "F:\\iPod_Control\\iTunes\\";
        public const string DbFilePath = "..\\..\\Data\\";
        public TestContext TestContext { get; set; }
        internal static MhbdReader Reader { get; set; }
        public static iTunesDb Db { get; set; }

        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext Context)
        {
            MhodReader.TrackTypes = new List<DataObjects>();
            MhodReader.PlayListTypes = new List<DataObjects>();
            MhodReader.MhipObjectTypes = new List<DataObjects>();
            Reader = MhbdReader.Open(DbFilePath + "iTunesDB");
            Db = Reader.Db;
        }

        public static int DbFileSize
        {
            get
            {
                return GetFileSize(DbFilePath + "iTunesDB");
            }
        }

        public static int GetFileSize(string FileName)
        {
            return Convert.ToInt32(new FileInfo(FileName).Length);
        }

        public static string DbFileName
        {
            get
            {
                return DbFilePath + "iTunesDB";
            }
        }
    }
}
