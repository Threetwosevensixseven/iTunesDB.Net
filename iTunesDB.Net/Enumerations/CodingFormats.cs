using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iTunesDB.Net.Enumerations
{
    // https://github.com/artm/ipod_db/blob/master/doc/ITunesDB%20-%20wikiPodLinux.html
    // type1 and type2 used to be one 2 byte field, but by it doesn't get reversed 
    // in the reversed endian iTunesDB for mobile phones, so it must be two fields.
    public enum CodingFormats
    {
        // type1 = 0x00, type2 = 0x01
        AAC = 0x0000,

        // type1 = 0x00, type2 = 0x01
        Mp3Cbr = 0x0100,

        // type1 = 0x01, type2 = 0x01 
        Mp3Vbr = 0x0101
    }
}
