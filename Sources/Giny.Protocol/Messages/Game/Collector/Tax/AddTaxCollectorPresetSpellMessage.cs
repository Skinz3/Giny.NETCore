using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AddTaxCollectorPresetSpellMessage : NetworkMessage
    {
        public const ushort Id = 2643;
        public override ushort MessageId => Id;

        public Uuid presetId;
        public TaxCollectorOrderedSpell spell;

        public AddTaxCollectorPresetSpellMessage()
        {
        }
        public AddTaxCollectorPresetSpellMessage(Uuid presetId, TaxCollectorOrderedSpell spell)
        {
            this.presetId = presetId;
            this.spell = spell;
        }
        public override void Serialize(IDataWriter writer)
        {
            presetId.Serialize(writer);
            spell.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            presetId = new Uuid();
            presetId.Deserialize(reader);
            spell = new TaxCollectorOrderedSpell();
            spell.Deserialize(reader);
        }

    }
}


