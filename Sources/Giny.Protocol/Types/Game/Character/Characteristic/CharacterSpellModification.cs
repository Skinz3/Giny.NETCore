using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterSpellModification  
    { 
        public const ushort Id = 5654;
        public virtual ushort TypeId => Id;

        public byte modificationType;
        public short spellId;
        public CharacterCharacteristicDetailed value;

        public CharacterSpellModification()
        {
        }
        public CharacterSpellModification(byte modificationType,short spellId,CharacterCharacteristicDetailed value)
        {
            this.modificationType = modificationType;
            this.spellId = spellId;
            this.value = value;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)modificationType);
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
            value.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            modificationType = (byte)reader.ReadByte();
            if (modificationType < 0)
            {
                throw new System.Exception("Forbidden value (" + modificationType + ") on element of CharacterSpellModification.modificationType.");
            }

            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of CharacterSpellModification.spellId.");
            }

            value = new CharacterCharacteristicDetailed();
            value.Deserialize(reader);
        }


    }
}








