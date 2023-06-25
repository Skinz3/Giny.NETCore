using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SpellForPreset  
    { 
        public const ushort Id = 3545;
        public virtual ushort TypeId => Id;

        public short spellId;
        public short[] shortcuts;

        public SpellForPreset()
        {
        }
        public SpellForPreset(short spellId,short[] shortcuts)
        {
            this.spellId = spellId;
            this.shortcuts = shortcuts;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
            writer.WriteShort((short)shortcuts.Length);
            for (uint _i2 = 0;_i2 < shortcuts.Length;_i2++)
            {
                writer.WriteShort((short)shortcuts[_i2]);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            int _val2 = 0;
            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of SpellForPreset.spellId.");
            }

            uint _shortcutsLen = (uint)reader.ReadUShort();
            shortcuts = new short[_shortcutsLen];
            for (uint _i2 = 0;_i2 < _shortcutsLen;_i2++)
            {
                _val2 = (int)reader.ReadShort();
                shortcuts[_i2] = (short)_val2;
            }

        }


    }
}








