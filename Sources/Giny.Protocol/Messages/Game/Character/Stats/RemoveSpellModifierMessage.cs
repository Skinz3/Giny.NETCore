using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class RemoveSpellModifierMessage : NetworkMessage
    {
        public const ushort Id = 6246;
        public override ushort MessageId => Id;

        public double actorId;
        public byte modificationType;
        public short spellId;

        public RemoveSpellModifierMessage()
        {
        }
        public RemoveSpellModifierMessage(double actorId, byte modificationType, short spellId)
        {
            this.actorId = actorId;
            this.modificationType = modificationType;
            this.spellId = spellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actorId < -9007199254740992 || actorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element actorId.");
            }

            writer.WriteDouble((double)actorId);
            writer.WriteByte((byte)modificationType);
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            actorId = (double)reader.ReadDouble();
            if (actorId < -9007199254740992 || actorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element of RemoveSpellModifierMessage.actorId.");
            }

            modificationType = (byte)reader.ReadByte();
            if (modificationType < 0)
            {
                throw new System.Exception("Forbidden value (" + modificationType + ") on element of RemoveSpellModifierMessage.modificationType.");
            }

            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of RemoveSpellModifierMessage.spellId.");
            }

        }

    }
}


