using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PresetsContainerPreset : Preset  
    { 
        public new const ushort Id = 9466;
        public override ushort TypeId => Id;

        public Preset[] presets;

        public PresetsContainerPreset()
        {
        }
        public PresetsContainerPreset(Preset[] presets,short id)
        {
            this.presets = presets;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)presets.Length);
            for (uint _i1 = 0;_i1 < presets.Length;_i1++)
            {
                writer.WriteShort((short)(presets[_i1] as Preset).TypeId);
                (presets[_i1] as Preset).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            Preset _item1 = null;
            base.Deserialize(reader);
            uint _presetsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _presetsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<Preset>((short)_id1);
                _item1.Deserialize(reader);
                presets[_i1] = _item1;
            }

        }


    }
}








