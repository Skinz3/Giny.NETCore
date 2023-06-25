using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ForgettableSpellItem : SpellItem  
    { 
        public new const ushort Id = 6689;
        public override ushort TypeId => Id;

        public bool available;

        public ForgettableSpellItem()
        {
        }
        public ForgettableSpellItem(bool available,int spellId,short spellLevel)
        {
            this.available = available;
            this.spellId = spellId;
            this.spellLevel = spellLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)available);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            available = (bool)reader.ReadBoolean();
        }


    }
}








