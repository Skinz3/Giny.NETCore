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
        public const ushort Id = 4699;
        public override ushort MessageId => Id;

        public double actorId;
        public byte actionType;
        public byte modifierType;
        public short spellId;

        public RemoveSpellModifierMessage()
        {
        }
        public RemoveSpellModifierMessage(double actorId, byte actionType, byte modifierType, short spellId)
        {
            this.actorId = actorId;
            this.actionType = actionType;
            this.modifierType = modifierType;
            this.spellId = spellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actorId < -9007199254740992 || actorId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element actorId.");
            }

            writer.WriteDouble((double)actorId);
            writer.WriteByte((byte)actionType);
            writer.WriteByte((byte)modifierType);
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

            actionType = (byte)reader.ReadByte();
            if (actionType < 0)
            {
                throw new System.Exception("Forbidden value (" + actionType + ") on element of RemoveSpellModifierMessage.actionType.");
            }

            modifierType = (byte)reader.ReadByte();
            if (modifierType < 0)
            {
                throw new System.Exception("Forbidden value (" + modifierType + ") on element of RemoveSpellModifierMessage.modifierType.");
            }

            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of RemoveSpellModifierMessage.spellId.");
            }

        }

    }
}


