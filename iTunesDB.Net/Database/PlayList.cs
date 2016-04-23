using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesDB.Net.Attributes;

namespace iTunesDB.Net.Database
{
    public class PlayList : List<PlayListItem>, IDbObject
    {
        [DataObject("Title")]
        public string Name { get; set; }
    }
}
