using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FullStatsPreset : Preset  
    { 
        public new const ushort Id = 3552;
        public override ushort TypeId => Id;

        public CharacterCharacteristicForPreset[] stats;

        public FullStatsPreset()
        {
        }
        public FullStatsPreset(CharacterCharacteristicForPreset[] stats,short id)
        {
            this.stats = stats;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)stats.Length);
            for (uint _i1 = 0;_i1 < stats.Length;_i1++)
            {
                (stats[_i1] as CharacterCharacteristicForPreset).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            CharacterCharacteristicForPreset _item1 = null;
            base.Deserialize(reader);
            uint _statsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _statsLen;_i1++)
            {
                _item1 = new CharacterCharacteristicForPreset();
                _item1.Deserialize(reader);
                stats[_i1] = _item1;
            }

        }


    }
}








