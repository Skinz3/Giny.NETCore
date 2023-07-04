using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class PaginationAnswerAbstractMessage : NetworkMessage
    {
        public const ushort Id = 500;
        public override ushort MessageId => Id;

        public double offset;
        public uint count;
        public uint total;

        public PaginationAnswerAbstractMessage()
        {
        }
        public PaginationAnswerAbstractMessage(double offset, uint count, uint total)
        {
            this.offset = offset;
            this.count = count;
            this.total = total;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (offset < 0 || offset > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + offset + ") on element offset.");
            }

            writer.WriteDouble((double)offset);
            if (count < 0 || count > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + count + ") on element count.");
            }

            writer.WriteUInt((uint)count);
            if (total < 0 || total > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + total + ") on element total.");
            }

            writer.WriteUInt((uint)total);
        }
        public override void Deserialize(IDataReader reader)
        {
            offset = (double)reader.ReadDouble();
            if (offset < 0 || offset > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + offset + ") on element of PaginationAnswerAbstractMessage.offset.");
            }

            count = (uint)reader.ReadUInt();
            if (count < 0 || count > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + count + ") on element of PaginationAnswerAbstractMessage.count.");
            }

            total = (uint)reader.ReadUInt();
            if (total < 0 || total > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + total + ") on element of PaginationAnswerAbstractMessage.total.");
            }

        }

    }
}


