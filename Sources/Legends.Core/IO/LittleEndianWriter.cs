using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Legends.Core.IO
{
    public class LittleEndianWriter : IDisposable
    {
        private BinaryWriter m_writer;

        public long BytesAvailable
        {
            get
            {
                return this.m_writer.BaseStream.Length - this.m_writer.BaseStream.Position;
            }
        }
        public int Position
        {
            get
            {
                return (int)m_writer.BaseStream.Position;
            }
            set
            {
                this.m_writer.BaseStream.Position = value;
            }
        }

        public byte[] Data
        {
            get
            {
                long position = this.m_writer.BaseStream.Position;
                byte[] array = new byte[this.m_writer.BaseStream.Length];
                this.m_writer.BaseStream.Position = 0L;
                this.m_writer.BaseStream.Read(array, 0, (int)this.m_writer.BaseStream.Length);
                this.m_writer.BaseStream.Position = position;
                return array;
            }
        }

        

        public void Fill(byte fillWith, int length)
        {
            for (int i = 0; i < length; i++)
            {
                WriteByte(fillWith);
            }
        }

        public LittleEndianWriter()
        {
            this.m_writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8);
        }
        public LittleEndianWriter(Stream stream)
        {
            m_writer = new BinaryWriter(stream);
        }
        public LittleEndianWriter(byte[] tab)
        {
            this.m_writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8);

            foreach (var b in tab)
            {
                WriteByte(b);
            }
        }
        public void WriteShort(short @short)
        {
            this.m_writer.Write(@short);
        }

        public void WriteInt(int @int)
        {
            this.m_writer.Write(@int);
        }

        public void WriteLong(long @long)
        {
            this.m_writer.Write(@long);
        }

        public void WriteUShort(ushort @ushort)
        {
            this.m_writer.Write(@ushort);
        }

        public void WriteUInt(uint @uint)
        {
            this.m_writer.Write(@uint);
        }

        public void WriteULong(ulong @ulong)
        {
            this.m_writer.Write(@ulong);
        }

        public void WriteByte(byte @byte)
        {
            this.m_writer.Write(@byte);
        }

        public void WriteSByte(SByte @byte)
        {
            this.m_writer.Write(@byte);
        }

        public void WriteBool(bool @bool)
        {
            if (@bool)
            {
                this.m_writer.Write((byte)1);
            }
            else
            {
                this.m_writer.Write((byte)0);
            }
        }

        public void WriteChar(char @char)
        {
            this.m_writer.Write(@char);
        }

        public void WriteDouble(double @double)
        {
            this.m_writer.Write(@double);
        }

        public void WriteFloat(float @float)
        {
            this.m_writer.Write(@float);
        }

        public void WriteString(string str)
        {
            this.m_writer.Write(str);
        }
        public void WriteString(string str,int length)
        {
            foreach (var b in Encoding.UTF8.GetBytes(str))
                m_writer.Write((byte)b);

            this.Fill(0, length - str.Length);
        }
        public void WriteSizedString(string str)
        {
            var data = string.IsNullOrEmpty(str) ? new byte[0] : Encoding.UTF8.GetBytes(str);
            var count = data.Length;
            WriteInt(count);
            WriteBytes(data);
        }

        public void WriteBytes(byte[] data)
        {
            this.m_writer.Write(data);
        }

        public void Seek(int offset)
        {
            this.Seek(offset, SeekOrigin.Begin);
        }

        public void Seek(int offset, SeekOrigin seekOrigin)
        {
            this.m_writer.BaseStream.Seek((long)offset, seekOrigin);
        }

        public void Clear()
        {
            this.m_writer = new BinaryWriter(new MemoryStream(), Encoding.UTF8);
        }

        public void Dispose()
        {
            this.m_writer.Flush();
            this.m_writer = null;
        }
        public override string ToString()
        {
            return string.Join(",", Data);
        }
    }
}
