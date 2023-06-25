using Giny.Core.IO;
using Giny.Core.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Network.Messages
{
    public class MessagePart
    {
        private byte[] m_data;
        private byte[] m_completeData;

        public bool IsValid
        {
            get
            {
                int num;
                if (this.Header.HasValue)
                {
                    int? length1 = this.Length;
                    if (length1.HasValue)
                    {
                        length1 = this.Length;
                        int length2 = this.Data.Length;
                        num = length1.GetValueOrDefault() == length2 ? (length1.HasValue ? 1 : 0) : 0;
                        goto label_4;
                    }
                }
                num = 0;
                label_4:
                return num != 0;
            }
        }

        public int? Header { get; private set; }

        public int? MessageId
        {
            get
            {
                if (!this.Header.HasValue)
                    return new int?();
                int? header = this.Header;
                return header.HasValue ? new int?(header.GetValueOrDefault() >> 2) : new int?();
            }
        }

        public int? LengthBytesCount
        {
            get
            {
                if (!this.Header.HasValue)
                    return new int?();
                int? header = this.Header;
                return header.HasValue ? new int?(header.GetValueOrDefault() & 3) : new int?();
            }
        }

        public int? Length { get; private set; }

        public byte[] Data
        {
            get
            {
                return this.m_data;
            }
            private set
            {
                this.m_data = value;
            }
        }

        public byte[] CompleteData
        {
            get
            {
                return this.m_completeData;
            }
            private set
            {
                this.m_completeData = value;
            }
        }

        public bool Build(BigEndianReader reader)
        {
            if (this.IsValid)
                return true;
            this.m_completeData = reader.Data;
            int? nullable1;
            int num1;
            if (reader.BytesAvailable >= 2L)
            {
                nullable1 = this.Header;
                num1 = !nullable1.HasValue ? 1 : 0;
            }
            else
                num1 = 0;
            if (num1 != 0)
                this.Header = reader.ReadUShort();
            reader.ReadUInt();
            nullable1 = this.LengthBytesCount;
            long? nullable2;
            int num2;
            if (nullable1.HasValue)
            {
                long bytesAvailable = reader.BytesAvailable;
                nullable1 = this.LengthBytesCount;
                nullable2 = nullable1.HasValue ? new long?((long)nullable1.GetValueOrDefault()) : new long?();
                long valueOrDefault = nullable2.GetValueOrDefault();
                if ((bytesAvailable >= valueOrDefault ? (nullable2.HasValue ? 1 : 0) : 0) != 0)
                {
                    nullable1 = this.Length;
                    num2 = !nullable1.HasValue ? 1 : 0;
                    goto label_11;
                }
            }
            num2 = 0;
            label_11:
            if (num2 != 0)
            {
                nullable1 = this.LengthBytesCount;
                int num3 = 0;
                int num4;
                if ((nullable1.GetValueOrDefault() < num3 ? (nullable1.HasValue ? 1 : 0) : 0) == 0)
                {
                    nullable1 = this.LengthBytesCount;
                    int num5 = 3;
                    num4 = nullable1.GetValueOrDefault() > num5 ? (nullable1.HasValue ? 1 : 0) : 0;
                }
                else
                    num4 = 1;
                if (num4 != 0)
                    throw new Exception("Malformated Message Header, invalid bytes number to read message length (inferior to 0 or superior to 3)");
                this.Length = new int?(0);
                nullable1 = this.LengthBytesCount;
                for (int index = nullable1.Value - 1; index >= 0; --index)
                {
                    nullable1 = this.Length;
                    int num5 = (int)reader.ReadByte() << index * 8;
                    this.Length = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() | num5) : new int?();
                }
            }
            int num6;
            if (this.Data == null)
            {
                nullable1 = this.Length;
                num6 = nullable1.HasValue ? 1 : 0;
            }
            else
                num6 = 0;
            if (num6 != 0)
            {
                nullable1 = this.Length;
                int num3 = 0;
                if (nullable1.GetValueOrDefault() == num3 && nullable1.HasValue)
                    this.Data = new byte[0];
                long bytesAvailable1 = reader.BytesAvailable;
                nullable1 = this.Length;
                nullable2 = nullable1.HasValue ? new long?((long)nullable1.GetValueOrDefault()) : new long?();
                long valueOrDefault = nullable2.GetValueOrDefault();
                if (bytesAvailable1 >= valueOrDefault && nullable2.HasValue)
                {
                    IDataReader bigEndianReader = reader;
                    nullable1 = this.Length;
                    int n = nullable1.Value;
                    this.Data = bigEndianReader.ReadBytes(n);
                }
                else
                {
                    nullable1 = this.Length;
                    nullable2 = nullable1.HasValue ? new long?((long)nullable1.GetValueOrDefault()) : new long?();
                    long bytesAvailable2 = reader.BytesAvailable;
                    if (nullable2.GetValueOrDefault() > bytesAvailable2 && nullable2.HasValue)
                        this.Data = reader.ReadBytes((int)reader.BytesAvailable);
                }
            }
            int num7;
            if (this.Data != null)
            {
                nullable1 = this.Length;
                if (nullable1.HasValue)
                {
                    int length = this.Data.Length;
                    nullable1 = this.Length;
                    int valueOrDefault = nullable1.GetValueOrDefault();
                    num7 = length < valueOrDefault ? (nullable1.HasValue ? 1 : 0) : 0;
                    goto label_35;
                }
            }
            num7 = 0;
            label_35:
            if (num7 != 0)
            {
                int num3 = 0;
                long num4 = (long)this.Data.Length + reader.BytesAvailable;
                nullable1 = this.Length;
                nullable2 = nullable1.HasValue ? new long?((long)nullable1.GetValueOrDefault()) : new long?();
                long valueOrDefault1 = nullable2.GetValueOrDefault();
                if (num4 < valueOrDefault1 && nullable2.HasValue)
                {
                    num3 = (int)reader.BytesAvailable;
                }
                else
                {
                    long num5 = (long)this.Data.Length + reader.BytesAvailable;
                    nullable1 = this.Length;
                    nullable2 = nullable1.HasValue ? new long?((long)nullable1.GetValueOrDefault()) : new long?();
                    long valueOrDefault2 = nullable2.GetValueOrDefault();
                    if (num5 >= valueOrDefault2 && nullable2.HasValue)
                    {
                        nullable1 = this.Length;
                        num3 = nullable1.Value - this.Data.Length;
                    }
                }
                if ((uint)num3 > 0U)
                {
                    int length = this.Data.Length;
                    Array.Resize<byte>(ref this.m_data, this.Data.Length + num3);
                    Array.Copy((Array)reader.ReadBytes(num3), 0, (Array)this.Data, length, num3);
                }
            }
            return this.IsValid;
        }
    }
}
