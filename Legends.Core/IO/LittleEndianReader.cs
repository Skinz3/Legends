using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.IO
{
    public class LittleEndianReader : IDisposable
    {
        private BinaryReader m_reader;

        public int BytesAvailable
        {
            get
            {
                return (int)(this.m_reader.BaseStream.Length - this.m_reader.BaseStream.Position);
            }
        }
        public int Position
        {
            get
            {
                return (int)this.m_reader.BaseStream.Position;
            }
        }
        public Stream BaseStream
        {
            get
            {
                return this.m_reader.BaseStream;
            }
        }

        public LittleEndianReader()
        {
            this.m_reader = new BinaryReader(new MemoryStream(), Encoding.UTF8);
        }

        public LittleEndianReader(Stream stream)
        {
            this.m_reader = new BinaryReader(stream, Encoding.UTF8);
        }

        public LittleEndianReader(byte[] tab)
        {
            this.m_reader = new BinaryReader(new MemoryStream(tab), Encoding.UTF8);
        }
        public short ReadShort()
        {
            return m_reader.ReadInt16();
        }

        public int ReadInt()
        {
            return m_reader.ReadInt32();
        }

        public long ReadLong()
        {
            return m_reader.ReadInt64();
        }

        public ushort ReadUShort()
        {
            return m_reader.ReadUInt16();
        }

        public uint ReadUInt()
        {
            return m_reader.ReadUInt32();
        }

        public ulong ReadULong()
        {
            return m_reader.ReadUInt64();
        }

        public byte ReadByte()
        {
            return this.m_reader.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return this.m_reader.ReadSByte();
        }

        public byte[] ReadBytes(int n)
        {
            return this.m_reader.ReadBytes(n);
        }

        public bool ReadBoolean()
        {
            return this.m_reader.ReadByte() == 1;
        }

        public char ReadChar()
        {
            return (char)this.ReadUShort();
        }

        public double ReadDouble()
        {
            return m_reader.ReadDouble();
        }

        public float ReadFloat()
        {
            return m_reader.ReadSingle();
        }
        public string[] ReadUTFLines()
        {
            List<string> lines = new List<string>();

            foreach (var line in m_reader.ReadString().Split(new string[] { "\r\n" }, StringSplitOptions.None)) 
            {
                lines.Add(line);
            }
            return lines.ToArray();
        }
        public string ReadUTF()
        {
            return m_reader.ReadString();
        }

        public void SkipBytes(int n)
        {
            for (int i = 0; i < n; i++)
            {
                this.m_reader.ReadByte();
            }
        }

        public void Seek(int offset, SeekOrigin seekOrigin = SeekOrigin.Begin)
        {
            this.m_reader.BaseStream.Seek((long)offset, seekOrigin);
        }

        public void Add(byte[] data, int offset, int count)
        {
            long position = this.m_reader.BaseStream.Position;
            this.m_reader.BaseStream.Position = this.m_reader.BaseStream.Length;
            this.m_reader.BaseStream.Write(data, offset, count);
            this.m_reader.BaseStream.Position = position;
        }

        public void Dispose()
        {
            this.m_reader = null;
        }
    }
}
