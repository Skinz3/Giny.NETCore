using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class MoveTaxCollectorPresetSpellMessage : NetworkMessage
    {
        public const ushort Id = 1282;
        public override ushort MessageId => Id;

        public Uuid presetId;
        public byte movedFrom;
        public byte movedTo;

        public MoveTaxCollectorPresetSpellMessage()
        {
        }
        public MoveTaxCollectorPresetSpellMessage(Uuid presetId, byte movedFrom, byte movedTo)
        {
            this.presetId = presetId;
            this.movedFrom = movedFrom;
            this.movedTo = movedTo;
        }
        public override void Serialize(IDataWriter writer)
        {
            presetId.Serialize(writer);
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
            presetId = new Uuid();
            presetId.Deserialize(reader);
            movedFrom = (byte)reader.ReadByte();
            if (movedFrom < 0)
            {
                throw new System.Exception("Forbidden value (" + movedFrom + ") on element of MoveTaxCollectorPresetSpellMessage.movedFrom.");
            }

            movedTo = (byte)reader.ReadByte();
            if (movedTo < 0)
            {
                throw new System.Exception("Forbidden value (" + movedTo + ") on element of MoveTaxCollectorPresetSpellMessage.movedTo.");
            }

        }

    }
}


