using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ForgettableSpellsPreset : Preset  
    { 
        public new const ushort Id = 5549;
        public override ushort TypeId => Id;

        public SpellsPreset baseSpellsPreset;
        public SpellForPreset[] forgettableSpells;

        public ForgettableSpellsPreset()
        {
        }
        public ForgettableSpellsPreset(SpellsPreset baseSpellsPreset,SpellForPreset[] forgettableSpells,short id)
        {
            this.baseSpellsPreset = baseSpellsPreset;
            this.forgettableSpells = forgettableSpells;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            baseSpellsPreset.Serialize(writer);
            writer.WriteShort((short)forgettableSpells.Length);
            for (uint _i2 = 0;_i2 < forgettableSpells.Length;_i2++)
            {
                (forgettableSpells[_i2] as SpellForPreset).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            SpellForPreset _item2 = null;
            base.Deserialize(reader);
            baseSpellsPreset = new SpellsPreset();
            baseSpellsPreset.Deserialize(reader);
            uint _forgettableSpellsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _forgettableSpellsLen;_i2++)
            {
                _item2 = new SpellForPreset();
                _item2.Deserialize(reader);
                forgettableSpells[_i2] = _item2;
            }

        }


    }
}








