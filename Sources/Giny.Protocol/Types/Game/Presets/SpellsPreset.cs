using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SpellsPreset : Preset  
    { 
        public new const ushort Id = 8652;
        public override ushort TypeId => Id;

        public SpellForPreset[] spells;

        public SpellsPreset()
        {
        }
        public SpellsPreset(SpellForPreset[] spells,short id)
        {
            this.spells = spells;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)spells.Length);
            for (uint _i1 = 0;_i1 < spells.Length;_i1++)
            {
                (spells[_i1] as SpellForPreset).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            SpellForPreset _item1 = null;
            base.Deserialize(reader);
            uint _spellsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _spellsLen;_i1++)
            {
                _item1 = new SpellForPreset();
                _item1.Deserialize(reader);
                spells[_i1] = _item1;
            }

        }


    }
}








