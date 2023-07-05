using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class MoveTaxCollectorOrderedSpellMessage : NetworkMessage
    {
        public const ushort Id = 234;
        public override ushort MessageId => Id;

        public double taxCollectorId;
        public byte movedFrom;
        public byte movedTo;

        public MoveTaxCollectorOrderedSpellMessage()
        {
        }
        public MoveTaxCollectorOrderedSpellMessage(double taxCollectorId, byte movedFrom, byte movedTo)
        {
            this.taxCollectorId = taxCollectorId;
            this.movedFrom = movedFrom;
            this.movedTo = movedTo;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element taxCollectorId.");
            }

            writer.WriteDouble((double)taxCollectorId);
            if (movedFrom < 0)
            {
                throw new System.Exception("Forbidden value (" + movedFrom + ") on element movedFrom.");
            }

            writer.WriteByte((byte)movedFrom);
            if (movedTo < 0)
            {
                throw new System.Exception("Forbidden value (" + movedTo + ") on element movedTo.");
            }

            writer.WriteByte((byte)movedTo);
        }
        public override void Deserialize(IDataReader reader)
        {
            taxCollectorId = (double)reader.ReadDouble();
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element of MoveTaxCollectorOrderedSpellMessage.taxCollectorId.");
            }

            movedFrom = (byte)reader.ReadByte();
            if (movedFrom < 0)
            {
                throw new System.Exception("Forbidden value (" + movedFrom + ") on element of MoveTaxCollectorOrderedSpellMessage.movedFrom.");
            }

            movedTo = (byte)reader.ReadByte();
            if (movedTo < 0)
            {
                throw new System.Exception("Forbidden value (" + movedTo + ") on element of MoveTaxCollectorOrderedSpellMessage.movedTo.");
            }

        }

    }
}


