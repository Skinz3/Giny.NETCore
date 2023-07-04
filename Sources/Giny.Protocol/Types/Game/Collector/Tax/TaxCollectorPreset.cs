using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class TaxCollectorPreset
    {
        public const ushort Id = 2340;
        public virtual ushort TypeId => Id;

        public uuid presetId;
        public TaxCollectorOrderedSpell[] spells;
        public CharacterCharacteristics characteristics;

        public TaxCollectorPreset()
        {
        }
        public TaxCollectorPreset(uuid presetId, TaxCollectorOrderedSpell[] spells, CharacterCharacteristics characteristics)
        {
            this.presetId = presetId;
            this.spells = spells;
            this.characteristics = characteristics;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            presetId.Serialize(writer);
            writer.WriteShort((short)spells.Length);
            for (uint _i2 = 0; _i2 < spells.Length; _i2++)
            {
                (spells[_i2] as TaxCollectorOrderedSpell).Serialize(writer);
            }

            characteristics.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            TaxCollectorOrderedSpell _item2 = null;
            presetId = new uuid();
            presetId.Deserialize(reader);
            uint _spellsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0; _i2 < _spellsLen; _i2++)
            {
                _item2 = new TaxCollectorOrderedSpell();
                _item2.Deserialize(reader);
                spells[_i2] = _item2;
            }

            characteristics = new CharacterCharacteristics();
            characteristics.Deserialize(reader);
        }


    }
}


