using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorStateUpdateMessage : NetworkMessage
    {
        public const ushort Id = 9183;
        public override ushort MessageId => Id;

        public double uniqueId;
        public byte state;

        public TaxCollectorStateUpdateMessage()
        {
        }
        public TaxCollectorStateUpdateMessage(double uniqueId, byte state)
        {
            this.uniqueId = uniqueId;
            this.state = state;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (uniqueId < 0 || uniqueId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uniqueId + ") on element uniqueId.");
            }

            writer.WriteDouble((double)uniqueId);
            writer.WriteByte((byte)state);
        }
        public override void Deserialize(IDataReader reader)
        {
            uniqueId = (double)reader.ReadDouble();
            if (uniqueId < 0 || uniqueId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uniqueId + ") on element of TaxCollectorStateUpdateMessage.uniqueId.");
            }

            state = (byte)reader.ReadByte();
            if (state < 0)
            {
                throw new System.Exception("Forbidden value (" + state + ") on element of TaxCollectorStateUpdateMessage.state.");
            }

        }

    }
}


