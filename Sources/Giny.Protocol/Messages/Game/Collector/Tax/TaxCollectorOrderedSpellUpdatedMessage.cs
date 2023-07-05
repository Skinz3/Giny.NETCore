using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorOrderedSpellUpdatedMessage : NetworkMessage
    {
        public const ushort Id = 7681;
        public override ushort MessageId => Id;

        public double taxCollectorId;
        public TaxCollectorOrderedSpell[] taxCollectorSpells;

        public TaxCollectorOrderedSpellUpdatedMessage()
        {
        }
        public TaxCollectorOrderedSpellUpdatedMessage(double taxCollectorId, TaxCollectorOrderedSpell[] taxCollectorSpells)
        {
            this.taxCollectorId = taxCollectorId;
            this.taxCollectorSpells = taxCollectorSpells;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element taxCollectorId.");
            }

            writer.WriteDouble((double)taxCollectorId);
            writer.WriteShort((short)taxCollectorSpells.Length);
            for (uint _i2 = 0; _i2 < taxCollectorSpells.Length; _i2++)
            {
                (taxCollectorSpells[_i2] as TaxCollectorOrderedSpell).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            TaxCollectorOrderedSpell _item2 = null;
            taxCollectorId = (double)reader.ReadDouble();
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element of TaxCollectorOrderedSpellUpdatedMessage.taxCollectorId.");
            }

            uint _taxCollectorSpellsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0; _i2 < _taxCollectorSpellsLen; _i2++)
            {
                _item2 = new TaxCollectorOrderedSpell();
                _item2.Deserialize(reader);
                taxCollectorSpells[_i2] = _item2;
            }

        }

    }
}


