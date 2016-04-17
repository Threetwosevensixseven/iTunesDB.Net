using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTunesDB.Net.Attributes;
using iTunesDB.Net.Readers.DataObjects;

namespace iTunesDB.Net.Enumerations
{
    // http://read.pudn.com/downloads152/doc/666430/iTunesDB-format.pdf
    public enum DataObjects
    {
        Title = 1,
        Location = 2,	                            // This string should be less than 112 bytes/56 UTF-16 chars (not including the terminating \0) or the iPod will skip the song when trying to play it.
        Album = 3,
        Artist = 4,
        Genre = 5,
        FileType = 6,
        EQSetting = 7,
        Comment = 8,
        Category = 9,	                            // This is the category ("Technology", "Music", etc.) where the podcast was located. Introduced in db version 0x0d.
        Composer = 12,
        Grouping = 13,
        Description = 14,	                        // Such as podcast show notes. Accessible by selecting the center button on the iPod, where this string is displayed along with the song title, date, and timestamp. Introduced in db version 0x0d.
        PodcastEnclosureUrl = 15,	                // Note: this is either a UTF-8 or ASCII encoded string (NOT UTF-16). Also, there is no mhod::length value for this type. Introduced in db version 0x0d.
        PodcastRssUrl = 16,	                        // Note: this is either a UTF-8 or ASCII encoded string (NOT UTF-16). Also, there is no mhod::length value for this type. Introduced in db version 0x0d.
        ChapterData = 17,	                        // This is a m4a-style entry that is used to display subsongs within a mhit. Introduced in db version 0x0d.
        Subtitle = 18,	                            // Usually the same as Description. Introduced in db version 0x0d.
        Show = 19,	                                // For TV Shows only. Introduced in db version 0x0d?
        EpisodeNo = 20,	                            // For TV Shows only. Introduced in db version 0x0d?
        TvNetwork = 21,	                            // For TV Shows only. Introduced in db version 0x0d?
        AlbumArtist = 22,	                        // Introduced in db version 0x13?
        ArtistNameSort = 23,	                    // Artists with names like "The Beatles" will be in here as "Beatles, The". Introduced in db version 0x13?
        Keywords = 24,	                            // Appears to be a list of keywords pertaining to a track. Introduced in db version 0x13?
        Locale = 25,	                            // For TV show? (e.g. "us-tv||0|", v.0x18)
        TitleSort = 27,	                            // For sorting.
        AlbumSort = 28,	                            // For sorting.
        AlbumArtistSort = 29,	                    // For sorting.
        ComposerSort = 30,	                        // For sorting.
        TvShowSort = 31,	                        // For sorting.
        Unknown = 32,	                            // Created by iTunes 7.1 for video tracks. Binary field, no string.
        SmartPlaylistData = 50,
        SmartPlaylistRules = 51,
        LibraryPlaylistIndex = 52,
        LibraryPlaylistIndexLetterJumpTable = 53,
        SizingAndOrder = 100,	                    // iTunes uses it for column sizing info as well as an order indicator in playlists.
        AlbumListAlbum = 200,	                    // In Album List, iTunes 7.1
        AlbumListArtist = 201,	                    // In Album List, iTunes 7.1
        AlbumListArtistSort = 202,	                // For sorting. In Album List, iTunes 7.1
        AlbumListPodcastUrl = 203,
        AlbumListTVShow = 204	                    // In Album List, v. 0x18
    }
}
