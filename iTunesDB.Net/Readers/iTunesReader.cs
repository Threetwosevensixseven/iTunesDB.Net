using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using iTunesDB.Net.Database;
using iTunesDB.Net.Enumerations;
using iTunesDB.Net.Readers;
using iTunesDB.Net.Readers.DataObjects;

namespace iTunesDB.Net
{
    public abstract class iTunesReader
    {
        public abstract string ObjectID { get; }
        public abstract string[] ChildIDs { get; }
        public abstract Type DatabaseType { get; }
        public virtual int ObjectSize { get; set; }
        public virtual int TotalSize { get; set; }
        public virtual int BytesRead { get; set; }
        public virtual List<iTunesReader> Children { get; set; }
        public virtual IDbObject DbObject { get; set; }
        public virtual iTunesReader ParentReader { get; set; }

        public iTunesReader()
        {
            if (!(this is DataObjectReader))
            {
                BytesRead = 4;
                Children = new List<iTunesReader>();
            }
        }

        internal static iTunesReader CreateReader(string ObjectID, iTunesReader ParentReader)
        {
            foreach (var type in Assembly.GetAssembly(typeof(iTunesReader)).GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(iTunesReader))))
            {
                var obj = ((iTunesReader)Activator.CreateInstance(type));
                if (obj.ObjectID == ObjectID)
                {
                    obj.ParentReader = ParentReader;
                    return obj;
                }
            }
            throw new ArgumentException("Unknown object ID \"" + (ObjectID ?? "NULL") + "\".");
        }

        private IDbObject CreateDbObject()
        {
            if (DatabaseType == null)
                throw new InvalidOperationException("Unknown DB object type for ID \"" + (ObjectID ?? "NULL") + "\".");
            if (!DatabaseType.GetInterfaces().Contains(typeof(IDbObject)))
                throw new InvalidOperationException("DB object for ID \"" + (ObjectID ?? "NULL") + "\" does not implement IDbObject.");
            var obj = Activator.CreateInstance(DatabaseType) as IDbObject;
            if (obj == null)
                throw new InvalidOperationException("Unknown DB object for ID \"" + (ObjectID ?? "NULL") + "\".");
            return obj;
        }

        internal static DataObjectReader CreateDataObjectReader(DataObjects DataObjectKind, iTunesReader ParentReader)
        {
            foreach (var type in Assembly.GetAssembly(typeof(DataObjectReader)).GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(DataObjectReader))))
            {
                var obj = ((DataObjectReader)Activator.CreateInstance(type));
                if (obj.DataObjectTypes.Any(o => o == DataObjectKind))
                {
                    obj.ParentReader = ParentReader;
                    return obj;
                }
            }
            throw new ArgumentException("No data reader for data object \"" + DataObjectKind.ToString() + "\".");
        }

        protected bool Parse(BinaryReader Reader)
        {
            ReadObjectSize(Reader);
            ReadTotalSize(Reader);
            DbObject = CreateDbObject();
            if (!ParseiTunesObject(Reader)) return false;
            ReadRemainingBytes(Reader);
            if (!ParseChildren(Reader)) return false;
            return RaiseEvent(Reader);
        }

        protected virtual bool ParseiTunesObject(BinaryReader Reader)
        {
            return true;
        }

        protected virtual bool RaiseEvent(BinaryReader Reader)
        {
            return true;
        }

        private bool ParseChildren(BinaryReader Reader)
        {
            while (WhileCondition)
            {
                string id = Encoding.ASCII.GetString(Reader.ReadBytes(4));
                if (!ChildIDs.Any(t => t == id))
                {
                    throw new InvalidOperationException("Object ID \"" + (id ?? "NULL") +
                        "\" is not a valid child of object ID \"" + (ObjectID ?? "NULL") + "\".");
                }
                var child = CreateReader(id, this);
                Children.Add(child);
                if (!child.Parse(Reader)) return false;
            }
            return true;
        }

        protected virtual bool WhileCondition
        {
            get
            {
                return TotalSize > ObjectSize + childrenSize;
            }
        }

        private int childrenSize
        {
            get
            {
                var size = 0;
                foreach (var child in Children)
                    size += child.ObjectSize + child.childrenSize;
                return size;
            }
        }

        internal IEnumerable<iTunesReader> AllChildren
        {
            get
            {
                foreach (var child in Children)
                {
                    yield return child;
                    foreach (var grandchild in child.AllChildren)
                    {
                        yield return grandchild;
                    }
                }
            }
        }

        protected int ReadObjectSize(BinaryReader Reader)
        {
            ObjectSize = Reader.ReadInt32();
            BytesRead += sizeof(int);
            return ObjectSize;
        }

        protected int ReadTotalSize(BinaryReader Reader)
        {
            TotalSize = Reader.ReadInt32();
            BytesRead += sizeof(int);
            return TotalSize;
        }

        [SecuritySafeCritical]
        protected byte[] ReadRemainingBytes(BinaryReader Reader)
        {
            var val = Reader.ReadBytes(ObjectSize - BytesRead);
            BytesRead += val.Length;
            return val;
        }

        protected short ReadInt16(BinaryReader Reader)
        {
            var val = Reader.ReadInt16();
            BytesRead += sizeof(short);
            return val;
        }

        protected int ReadInt32(BinaryReader Reader)
        {
            var val = Reader.ReadInt32();
            BytesRead += sizeof(int);
            return val;
        }

        protected bool ReadBooleanInt32(BinaryReader Reader)
        {
            var val = Reader.ReadInt32();
            BytesRead += sizeof(int);
            return val == 1;
        }

        protected uint ReadUint32(BinaryReader Reader)
        {
            var val = Reader.ReadUInt32();
            BytesRead += sizeof(uint);
            return val;
        }

        protected uint? ReadUint32Nullable(BinaryReader Reader)
        {
            var val = Reader.ReadUInt32();
            BytesRead += sizeof(uint);
            if (val == 0) return null;
            return val;
        }

        protected UInt64 ReadUint64(BinaryReader Reader)
        {
            var val = Reader.ReadUInt64();
            BytesRead += sizeof(UInt64);
            return val;
        }

        protected DateTime ReadDateTime(BinaryReader Reader)
        {
            var val = Reader.ReadUInt32();
            BytesRead += sizeof(uint);
            return new DateTime(1904, 1, 1).AddSeconds(val);
        }

        protected DateTime? ReadDateTimeNullable(BinaryReader Reader)
        {
            var val = Reader.ReadUInt32();
            BytesRead += sizeof(uint);
            if (val == 0) return null;
            return new DateTime(1904, 1, 1).AddSeconds(val);
        }

        protected TimeSpan ReadTimeSpanMilliseconds(BinaryReader Reader)
        {
            var val = Reader.ReadUInt32();
            BytesRead += sizeof(uint);
            return new TimeSpan(val * TimeSpan.TicksPerMillisecond);
        }

        protected TimeSpan? ReadTimeSpanMillisecondsNullable(BinaryReader Reader)
        {
            var val = Reader.ReadUInt32();
            BytesRead += sizeof(uint);
            if (val == 0) return null;
            return new TimeSpan(val * TimeSpan.TicksPerMillisecond);
        }

        [SecuritySafeCritical]
        protected byte[] ReadBytes(BinaryReader Reader, int Count)
        {
            var val = Reader.ReadBytes(Count);
            BytesRead += Count;
            return val;
        }

        protected byte ReadByte(BinaryReader Reader)
        {
            var val = Reader.ReadByte();
            BytesRead += sizeof(byte);
            return val;
        }

        [SecuritySafeCritical]
        protected string ReadAscii(BinaryReader Reader, int Count, bool Reverse = false, bool Nullable = false, bool Trim = false)
        {
            var val = Reader.ReadBytes(Count);
            BytesRead += Count;
            if (Nullable && val.Sum(b => (uint)b) == 0) return "";
            if (Reverse) val = val.Reverse().ToArray();
            if (Trim) return Encoding.ASCII.GetString(val).Trim();
            else return Encoding.ASCII.GetString(val);
        }

        [SecuritySafeCritical]
        protected string ReadStringUtfDetect(BinaryReader Reader, int Count)
        {
            var val = Reader.ReadBytes(Count);
            BytesRead += Count;
            if (val.Any(b => b == 0)) return Encoding.Unicode.GetString(val);
            else return Encoding.UTF8.GetString(val);
        }

        protected bool TryParseEnum<T>(int Value, out T Result)
        {
            Result = default(T);
            bool success = Enum.IsDefined(typeof(T), Value);
            if (success) Result = (T)Enum.ToObject(typeof(T), Value);
            return success;
        }

        protected T ParseEnum<T>(int Value)
        {
            var result = default(T);
            bool success = Enum.IsDefined(typeof(T), Value);
            if (success) result = (T)Enum.ToObject(typeof(T), Value);
            else throw new InvalidOperationException("Unknown " + typeof(T).Name + " value \"" + Value + "\"");
            return result;
        }

        protected T ReadEnum<T>(BinaryReader Reader)
        {
            var val = Reader.ReadInt32();
            BytesRead += sizeof(int);
            return ParseEnum<T>(val);
        }

        protected bool ParseBool(byte Value, string FieldName)
        {
            var result = default(bool);
            if (Value == 0) result = false;
            else if (Value == 1) result = true;
            else throw new InvalidOperationException("Unknown " + (FieldName ?? "") + " value \"" + Value + "\"");
            return result;
        }

        public virtual IDbObject ParentDbObject
        {
            get
            {
                if (ParentReader == null) return null;
                return ParentReader.DbObject;
            }
        }

        public virtual IDbObject GrandparentDbObject
        {
            get
            {
                if (ParentReader == null) return null;
                if (ParentReader.ParentReader == null) return null;
                return ParentReader.ParentReader.DbObject;
            }
        }
    }
}

