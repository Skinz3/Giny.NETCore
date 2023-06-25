using Giny.Core.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.IO
{
    /// <summary>
    /// Much faster reader that only reads memory buffer
    /// </summary>
    public unsafe class FastBigEndianReader : IDataReader
    {
        public const int INT_SIZE = 32;
        public const int SHORT_SIZE = 16;
        public const int SHORT_MIN_VALUE = -0x8000;
        public const int SHORT_MAX_VALUE = 0x7FFF;
        public const int USHORT_MAX_VALUE = 0x10000;
        public const int CHUNCK_BIT_SIZE = 7;
        public static readonly int MAX_ENCODING_LENGHT = (int)Math.Ceiling((double)INT_SIZE / CHUNCK_BIT_SIZE);
        public const int MASK_10000000 = 0x80;
        public const int MASK_01111111 = 0x7F;

        private long m_position;
        private readonly byte[] m_buffer;
        private long m_maxPosition = -1;

        public byte[] Buffer
        {
            get { return m_buffer; }
        }

        public long Position
        {
            get { return m_position; }
            set
            {
                if (m_maxPosition > 0 && value > m_maxPosition)
                    throw new InvalidOperationException("Buffer overflow");

                m_position = value;
            }
        }

        public long MaxPosition
        {
            get { return m_maxPosition; }
            set { m_maxPosition = value; }
        }

        public long BytesAvailable
        {
            get { return (m_maxPosition > 0 ? m_maxPosition : m_buffer.Length) - Position; }
        }

        public byte[] Data => throw new NotImplementedException();

        public short ReadVarShort()
        {
            int value = 0;
            int size = 0;
            while (size < SHORT_SIZE)
            {
                var b = ReadByte();
                bool bit = (b & MASK_10000000) == MASK_10000000;
                if (size > 0)
                    value |= ((b & MASK_01111111) << size);
                else
                    value |= (b & MASK_01111111);
                size += CHUNCK_BIT_SIZE;
                if (!bit)
                {
                    if (value > SHORT_MAX_VALUE)
                        value = value - USHORT_MAX_VALUE;

                    return (short)value;
                }
            }

            throw new Exception("Overflow varint : too much data");
        }

        public ushort ReadVarUhShort()
        {
            return unchecked((ushort)ReadVarShort());
        }

        public int ReadVarInt()
        {
            int value = 0;
            int size = 0;
            while (size < INT_SIZE)
            {
                var b = ReadByte();
                bool bit = (b & MASK_10000000) == MASK_10000000;
                if (size > 0)
                    value |= ((b & MASK_01111111) << size);
                else
                    value |= (b & MASK_01111111);
                size += CHUNCK_BIT_SIZE;
                if (!bit)
                    return value;
            }

            throw new Exception("Overflow varint : too much data");
        }

        public uint ReadVarUhInt()
        {
            return unchecked((uint)ReadVarInt());
        }

        public long ReadVarLong()
        {
            int low = 0;
            int high = 0;
            int size = 0;
            byte lastByte = 0;
            while (size < 28)
            {
                lastByte = ReadByte();
                if ((lastByte & MASK_10000000) == MASK_10000000)
                {
                    low |= ((lastByte & MASK_01111111) << size);
                    size += 7;
                }
                else
                {
                    low |= lastByte << size;
                    return low;
                }
            }
            lastByte = ReadByte();
            if ((lastByte & MASK_10000000) == MASK_10000000)
            {
                low |= (lastByte & MASK_01111111) << size;
                high = (lastByte & MASK_01111111) >> 4;
                size = 3;
                while (size < 32)
                {
                    lastByte = ReadByte();
                    if ((lastByte & MASK_10000000) == MASK_10000000)
                        high |= (lastByte & MASK_01111111) << size;
                    else break;
                    size += 7;
                }
                high |= lastByte << size;
                return (low & 0xFFFFFFFF) | ((long)high << 32);
            }
            low |= lastByte << size;
            high = lastByte >> 4;
            return (low & 0xFFFFFFFF) | (long)high << 32;
        }

        public ulong ReadVarUhLong()
        {
            return unchecked((ulong)ReadVarLong());
        }

        public FastBigEndianReader(byte[] buffer)
        {
            m_buffer = buffer;
        }



        public byte ReadByte()
        {
            fixed (byte* pbyte = &m_buffer[Position++])
            {
                return *pbyte;
            }
        }

        public sbyte ReadSByte()
        {
            fixed (byte* pbyte = &m_buffer[Position++])
            {
                return (sbyte)*pbyte;
            }
        }

        public short ReadShort()
        {
            var position = Position;
            Position += 2;
            fixed (byte* pbyte = &m_buffer[position])
            {
                return (short)((*pbyte << 8) | (*(pbyte + 1)));
            }
        }

        public int ReadInt()
        {
            var position = Position;
            Position += 4;
            fixed (byte* pbyte = &m_buffer[position])
            {
                return (*pbyte << 24) | (*(pbyte + 1) << 16) | (*(pbyte + 2) << 8) | (*(pbyte + 3));
            }
        }

        public long ReadLong()
        {
            var position = Position;
            Position += 8;
            fixed (byte* pbyte = &m_buffer[position])
            {
                int i1 = (*pbyte << 24) | (*(pbyte + 1) << 16) | (*(pbyte + 2) << 8) | (*(pbyte + 3));
                int i2 = (*(pbyte + 4) << 24) | (*(pbyte + 5) << 16) | (*(pbyte + 6) << 8) | (*(pbyte + 7));
                return (uint)i2 | ((long)i1 << 32);
            }
        }

        public ushort ReadUShort()
        {
            return (ushort)ReadShort();
        }

        public uint ReadUInt()
        {
            return (uint)ReadInt();
        }

        public ulong ReadULong()
        {
            return (ulong)ReadLong();
        }

        public byte[] ReadBytes(int n)
        {
            if (BytesAvailable < n)
                throw new InvalidOperationException("Buffer overflow");

            var dst = new byte[n];
            fixed (byte* pSrc = &m_buffer[Position], pDst = dst)
            {
                byte* ps = pSrc;
                byte* pd = pDst;

                // Loop over the count in blocks of 4 bytes, copying an integer (4 bytes) at a time:
                for (int i = 0; i < n / 4; i++)
                {
                    *((int*)pd) = *((int*)ps);
                    pd += 4;
                    ps += 4;
                }

                // Complete the copy by moving any bytes that weren't moved in blocks of 4:
                for (int i = 0; i < n % 4; i++)
                {
                    *pd = *ps;
                    pd++;
                    ps++;
                }
            }

            Position += n;

            return dst;
        }

        public bool ReadBoolean()
        {
            return ReadByte() != 0;
        }

        public char ReadChar()
        {
            return (char)ReadShort();
        }

        public float ReadFloat()
        {
            int val = ReadInt();
            return *(float*)&val;
        }

        public double ReadDouble()
        {
            long val = ReadLong();
            return *(double*)&val;
        }

        public string ReadUTF()
        {
            ushort length = ReadUShort();

            byte[] bytes = ReadBytes(length);
            return Encoding.UTF8.GetString(bytes);
        }

        public string ReadUTFBytes(ushort len)
        {
            byte[] bytes = ReadBytes(len);
            return Encoding.UTF8.GetString(bytes);
        }

        public void Seek(int offset, SeekOrigin seekOrigin)
        {
            if (seekOrigin == SeekOrigin.Begin)
                Position = offset;
            else if (seekOrigin == SeekOrigin.End)
                Position = m_buffer.Length + offset;
            else if (seekOrigin == SeekOrigin.Current)
                Position += offset;
        }

        public void Seek(long offset, SeekOrigin seekOrigin)
        {
            if (seekOrigin == SeekOrigin.Begin)
                Position = offset;
            else if (seekOrigin == SeekOrigin.End)
                Position = m_buffer.Length + offset;
            else if (seekOrigin == SeekOrigin.Current)
                Position += offset;
        }

        public void SkipBytes(int n)
        {
            Position += n;
        }

        public void Dispose()
        {

        }
    }
}
