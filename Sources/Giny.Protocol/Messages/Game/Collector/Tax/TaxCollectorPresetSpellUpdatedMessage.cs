using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorPresetSpellUpdatedMessage : NetworkMessage
    {
        public const ushort Id = 5819;
        public override ushort MessageId => Id;

        public Uuid presetId;
        public TaxCollectorOrderedSpell[] taxCollectorSpells;

        public TaxCollectorPresetSpellUpdatedMessage()
        {
        }
        public TaxCollectorPresetSpellUpdatedMessage(Uuid presetId, TaxCollectorOrderedSpell[] taxCollectorSpells)
        {
            this.presetId = presetId;
            this.taxCollectorSpells = taxCollectorSpells;
        }
        public override void Serialize(IDataWriter writer)
        {
            presetId.Serialize(writer);
            writer.WriteShort((short)taxCollectorSpells.Length);
            for (uint _i2 = 0; _i2 < taxCollectorSpells.Length; _i2++)
            {
                (taxCollectorSpells[_i2] as TaxCollectorOrderedSpell).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            TaxCollectorOrderedSpell _item2 = null;
            presetId = new Uuid();
            presetId.Deserialize(reader);
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


