using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Attributes;
using iTunesDB.Net.Database;
using DO = iTunesDB.Net.Enumerations.DataObjects;

namespace iTunesDB.Net.Readers.DataObjects
{
    internal class NullReader : DataObjectReader
    {
        private static DO[] dataObjectTypes = new DO[] { 
            DO.PodcastEnclosureUrl, DO.PodcastRssUrl, DO.ChapterData, DO.Subtitle, DO.Show, 
            DO.EpisodeNo, DO.TvNetwork, DO.AlbumArtist, DO.ArtistNameSort, DO.Keywords, DO.Locale, DO.TitleSort, 
            DO.AlbumSort, DO.AlbumArtistSort, DO.ComposerSort, DO.TvShowSort, DO.Unknown, DO.SmartPlaylistData, 
            DO.SmartPlaylistRules, DO.LibraryPlaylistIndex, DO.LibraryPlaylistIndexLetterJumpTable, DO.SizingAndOrder, 
            DO.AlbumListAlbum, DO.AlbumListArtist, DO.AlbumListArtistSort, DO.AlbumListPodcastUrl, DO.AlbumListTVShow };
        public override DO[] DataObjectTypes { get { return dataObjectTypes; } }
        public override void ParseDataObject(BinaryReader Reader) { }
    }
}
