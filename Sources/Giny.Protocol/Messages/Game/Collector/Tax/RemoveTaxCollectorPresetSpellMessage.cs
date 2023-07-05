using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class RemoveTaxCollectorPresetSpellMessage : NetworkMessage
    {
        public const ushort Id = 2130;
        public override ushort MessageId => Id;

        public Uuid presetId;
        public byte slot;

        public RemoveTaxCollectorPresetSpellMessage()
        {
        }
        public RemoveTaxCollectorPresetSpellMessage(Uuid presetId, byte slot)
        {
            this.presetId = presetId;
            this.slot = slot;
        }
        public override void Serialize(IDataWriter writer)
        {
            presetId.Serialize(writer);
            if (slot < 0)
            {
                throw new System.Exception("Forbidden value (" + slot + ") on element slot.");
            }

            writer.WriteByte((byte)slot);
        }
        public override void Deserialize(IDataReader reader)
        {
            presetId = new Uuid();
            presetId.Deserialize(reader);
            slot = (byte)reader.ReadByte();
            if (slot < 0)
            {
                throw new System.Exception("Forbidden value (" + slot + ") on element of RemoveTaxCollectorPresetSpellMessage.slot.");
            }

        }

    }
}


