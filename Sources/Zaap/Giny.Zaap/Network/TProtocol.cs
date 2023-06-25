using Giny.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap.Network
{
    /// <summary>
    /// com.ankama.zaap
    /// org.apache.thrift.TBinaryProtocol
    /// </summary>
    public class TProtocol
    {
        private int VERSION_MASK;

        private int VERSION_1;

        private bool StrictRead
        {
            get;
            set;
        }
        private bool StrictWrite
        {
            get;
            set;
        }

        public TProtocol(bool strictRead = false, bool strictWrite = true)
        {
            unchecked
            {
                VERSION_MASK = (int)4294901760;
                VERSION_1 = (int)2147549184;
            }

            StrictRead = strictRead;
            StrictWrite = strictWrite;
        }

        public void WriteFieldStop(BigEndianWriter writer)
        {
            writer.WriteByte((byte)TType.STOP);
        }

        public TMessage ReadMessageBegin(BigEndianReader reader)
        {
         

            var val1 = reader.ReadInt();

            if (val1 < 0)
            {
                if ((val1 & VERSION_MASK) != VERSION_1)
                {
                    throw new Exception("Bad version in read message begin.");
                }

                TMessage result = new TMessage();
                result.Name = reader.ReadUTF7BitLength();
                result.Type = val1 & 255;
                result.SequenceId = reader.ReadInt();
                return result;
            }
            else
            {
                TMessage result = new TMessage();
                result.Name = reader.ReadUTFBytes((ushort)val1);
                result.Type = reader.ReadByte();
                result.SequenceId = reader.ReadInt();
                return result;
            }
          
        }

        public TField ReadFieldBegin(BigEndianReader reader)
        {
            var type = (TType)reader.ReadByte();
            int id = type == TType.STOP ? 0 : reader.ReadShort();
            return new TField("", type, id);
        }

        public void WriteMessageBegin(TMessage message, BigEndianWriter writer)
        {
            int loc2 = VERSION_1 | message.Type;
            writer.WriteInt(loc2);
            writer.WriteInt(message.Name.Length);
            writer.WriteUTFBytes(message.Name);
            writer.WriteInt(message.SequenceId);

        }
        public void WriteFieldBegin(TField field, BigEndianWriter writer)
        {
            writer.WriteByte((byte)field.Type);
            writer.WriteShort((short)field.Id);
        }
    }
}
