using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SpellItem : Item  
    { 
        public new const ushort Id = 8181;
        public override ushort TypeId => Id;

        public int spellId;
        public short spellLevel;

        public SpellItem()
        {
        }
        public SpellItem(int spellId,short spellLevel)
        {
            this.spellId = spellId;
            this.spellLevel = spellLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt((int)spellId);
            if (spellLevel < 1 || spellLevel > 32767)
            {
                throw new System.Exception("Forbidden value (" + spellLevel + ") on element spellLevel.");
            }

            writer.WriteShort((short)spellLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            spellId = (int)reader.ReadInt();
            spellLevel = (short)reader.ReadShort();
            if (spellLevel < 1 || spellLevel > 32767)
            {
                throw new System.Exception("Forbidden value (" + spellLevel + ") on element of SpellItem.spellLevel.");
            }

        }


    }
}








