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
        public const ushort Id = 4511;
        public override ushort MessageId => Id;

        public uuid presetId;
        public TaxCollectorOrderedSpell spell;

        public AddTaxCollectorPresetSpellMessage()
        {
        }
        public AddTaxCollectorPresetSpellMessage(uuid presetId, TaxCollectorOrderedSpell spell)
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
            presetId = new uuid();
            presetId.Deserialize(reader);
            spell = new TaxCollectorOrderedSpell();
            spell.Deserialize(reader);
        }

    }
}


