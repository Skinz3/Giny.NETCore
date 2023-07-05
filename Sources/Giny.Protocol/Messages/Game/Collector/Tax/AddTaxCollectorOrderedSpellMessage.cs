using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AddTaxCollectorOrderedSpellMessage : NetworkMessage
    {
        public const ushort Id = 61;
        public override ushort MessageId => Id;

        public double taxCollectorId;
        public TaxCollectorOrderedSpell spell;

        public AddTaxCollectorOrderedSpellMessage()
        {
        }
        public AddTaxCollectorOrderedSpellMessage(double taxCollectorId, TaxCollectorOrderedSpell spell)
        {
            this.taxCollectorId = taxCollectorId;
            this.spell = spell;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element taxCollectorId.");
            }

            writer.WriteDouble((double)taxCollectorId);
            spell.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            taxCollectorId = (double)reader.ReadDouble();
            if (taxCollectorId < 0 || taxCollectorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + taxCollectorId + ") on element of AddTaxCollectorOrderedSpellMessage.taxCollectorId.");
            }

            spell = new TaxCollectorOrderedSpell();
            spell.Deserialize(reader);
        }

    }
}


