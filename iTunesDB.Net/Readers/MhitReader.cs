using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using iTunesDB.Net.Database;
using iTunesDB.Net.Enumerations;

namespace iTunesDB.Net.Readers
{
    internal class MhitReader : iTunesReader
    {
        public override string ObjectID { get { return "mhit"; } }
        public override string[] ChildIDs { get { return new string[] { "mhod" }; } }
        public override Type DatabaseType { get { return typeof(Track); } }
        internal int TrackEntries { get; set; }

        protected override bool ParseiTunesObject(BinaryReader Reader)
        {
            var track = (Track)DbObject;
            var trackList = (TrackList)ParentDbObject;
            trackList.Add(track);

            TrackEntries = ReadInt32(Reader);
            track.TrackID = ReadInt32(Reader);
            track.Visible = ReadBooleanInt32(Reader);
            track.FileType = ReadAscii(Reader, 4, true, true, true);
            track.CodingFormat = ParseEnum<CodingFormats>(ReadByte(Reader) + (ReadByte(Reader) * 256));
            track.Compilation = ParseBool(ReadByte(Reader), "Compilation");
            track.Rating = ReadByte(Reader);
            track.LastModified = ReadDateTime(Reader);
            track.SizeBytes = ReadUint32(Reader);
            track.Length = ReadTimeSpanMilliseconds(Reader);
            track.TrackNumber = ReadInt32(Reader);
            track.TrackCount = ReadInt32(Reader);
            track.Year = ReadInt32(Reader);
            track.BitRate = ReadInt32(Reader);
            track.SampleRate = (ReadUint32(Reader)  + 0.0) / 0x10000;
            track.Volume = ReadInt32(Reader);
            track.StartTime = ReadTimeSpanMilliseconds(Reader);
            track.StopTime = ReadTimeSpanMillisecondsNullable(Reader);
            track.SoundCheck = ReadUint32(Reader);
            track.PlayCount = ReadInt32(Reader);
            track.PlayCountSinceLastSync = ReadInt32(Reader);
            track.LastPlayed = ReadDateTimeNullable(Reader);
            track.DiscNumber = ReadInt32(Reader);
            track.DiscCount = ReadInt32(Reader);
            track.UserID = ReadUint32Nullable(Reader);
            track.PersistentID = ReadUint64(Reader).ToString("X");
            track.Checked = !ReadBooleanInt32(Reader);
            track.ApplicationRating = ReadByte(Reader);
            track.BPM = ReadInt16(Reader);
            //if (track.BPM > 0) Debugger.Break();

            return true;
        }

        protected override bool RaiseEvent(BinaryReader Reader)
        {
            var dbReader = (MhbdReader)(ParentReader.ParentReader.ParentReader);
            var track = (Track)DbObject;
            return dbReader.OnTrackRead(ParentReader.TotalSize, dbReader.Db.Tracks.Count, track);
        }
    }
}

