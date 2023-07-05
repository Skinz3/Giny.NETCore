using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceModificationResultMessage : NetworkMessage
    {
        public const ushort Id = 5648;
        public override ushort MessageId => Id;

        public byte result;

        public AllianceModificationResultMessage()
        {
        }
        public AllianceModificationResultMessage(byte result)
        {
            this.result = result;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)result);
        }
        public override void Deserialize(IDataReader reader)
        {
            result = (byte)reader.ReadByte();
            if (result < 0)
            {
                throw new System.Exception("Forbidden value (" + result + ") on element of AllianceModificationResultMessage.result.");
            }

        }

    }
}


