using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class RemoveTaxCollectorOrderedSpellMessage : NetworkMessage
    {
        public const ushort Id = 4462;
        public override ushort MessageId => Id;

        public double taxCollectorId;
        public byte slot;

        public RemoveTaxCollectorOrderedSpellMessage()
        {
        }
        public RemoveTaxCollectorOrderedSpellMessage(double taxCollectorId, byte slot)
        {
            this.taxCollectorId = taxCollectorId;
            this.slot = slot;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element taxCollectorId.");
            }

            writer.WriteDouble((double)taxCollectorId);
            if (slot < 0)
            {
                throw new System.Exception("Forbidden value (" + slot + ") on element slot.");
            }

            writer.WriteByte((byte)slot);
        }
        public override void Deserialize(IDataReader reader)
        {
            taxCollectorId = (double)reader.ReadDouble();
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element of RemoveTaxCollectorOrderedSpellMessage.taxCollectorId.");
            }

            slot = (byte)reader.ReadByte();
            if (slot < 0)
            {
                throw new System.Exception("Forbidden value (" + slot + ") on element of RemoveTaxCollectorOrderedSpellMessage.slot.");
            }

        }

    }
}


