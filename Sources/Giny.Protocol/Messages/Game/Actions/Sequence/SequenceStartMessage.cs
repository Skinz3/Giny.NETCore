using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class SequenceStartMessage : NetworkMessage
    {
        public const ushort Id = 3397;
        public override ushort MessageId => Id;

        public byte sequenceType;
        public double authorId;

        public SequenceStartMessage()
        {
        }
        public SequenceStartMessage(byte sequenceType, double authorId)
        {
            this.sequenceType = sequenceType;
            this.authorId = authorId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)sequenceType);
            if (authorId < -9007199254740992 || authorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + authorId + ") on element authorId.");
            }

            writer.WriteDouble((double)authorId);
        }
        public override void Deserialize(IDataReader reader)
        {
            sequenceType = (byte)reader.ReadByte();
            authorId = (double)reader.ReadDouble();
            if (authorId < -9007199254740992 || authorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + authorId + ") on element of SequenceStartMessage.authorId.");
            }

        }

    }
}


