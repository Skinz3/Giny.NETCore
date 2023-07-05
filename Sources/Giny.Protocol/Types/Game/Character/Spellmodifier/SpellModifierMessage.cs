using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class SpellModifierMessage
    {
        public const ushort Id = 9395;
        public virtual ushort TypeId => Id;

        public short spellId;
        public byte actionType;
        public byte modifierType;
        public int context;
        public int equipment;

        public SpellModifierMessage()
        {
        }
        public SpellModifierMessage(short spellId, byte actionType, byte modifierType, int context, int equipment)
        {
            this.spellId = spellId;
            this.actionType = actionType;
            this.modifierType = modifierType;
            this.context = context;
            this.equipment = equipment;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
            writer.WriteByte((byte)actionType);
            writer.WriteByte((byte)modifierType);
            writer.WriteInt((int)context);
            writer.WriteInt((int)equipment);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of SpellModifierMessage.spellId.");
            }

            actionType = (byte)reader.ReadByte();
            if (actionType < 0)
            {
                throw new System.Exception("Forbidden value (" + actionType + ") on element of SpellModifierMessage.actionType.");
            }

            modifierType = (byte)reader.ReadByte();
            if (modifierType < 0)
            {
                throw new System.Exception("Forbidden value (" + modifierType + ") on element of SpellModifierMessage.modifierType.");
            }

            context = (int)reader.ReadInt();
            equipment = (int)reader.ReadInt();
        }


    }
}


