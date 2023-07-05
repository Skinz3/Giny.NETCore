using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class PrismAttackResultMessage : NetworkMessage
    {
        public const ushort Id = 4128;
        public override ushort MessageId => Id;

        public PrismGeolocalizedInformation prism;
        public byte result;

        public PrismAttackResultMessage()
        {
        }
        public PrismAttackResultMessage(PrismGeolocalizedInformation prism, byte result)
        {
            this.prism = prism;
            this.result = result;
        }
        public override void Serialize(IDataWriter writer)
        {
            prism.Serialize(writer);
            writer.WriteByte((byte)result);
        }
        public override void Deserialize(IDataReader reader)
        {
            prism = new PrismGeolocalizedInformation();
            prism.Deserialize(reader);
            result = (byte)reader.ReadByte();
            if (result < 0)
            {
                throw new System.Exception("Forbidden value (" + result + ") on element of PrismAttackResultMessage.result.");
            }

        }

    }
}


