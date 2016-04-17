using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesDB.Net.Enumerations;

namespace iTunesDB.Net.Database
{
    public class DataObject : IDbObject
    {
        public DataObjects Type { get; set; }
    }
}
