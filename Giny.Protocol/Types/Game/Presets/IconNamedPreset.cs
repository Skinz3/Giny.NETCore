using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class IconNamedPreset : PresetsContainerPreset  
    { 
        public new const ushort Id = 1919;
        public override ushort TypeId => Id;

        public short iconId;
        public string name;

        public IconNamedPreset()
        {
        }
        public IconNamedPreset(short iconId,string name,short id,Preset[] presets)
        {
            this.iconId = iconId;
            this.name = name;
            this.id = id;
            this.presets = presets;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (iconId < 0)
            {
                throw new System.Exception("Forbidden value (" + iconId + ") on element iconId.");
            }

            writer.WriteShort((short)iconId);
            writer.WriteUTF((string)name);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            iconId = (short)reader.ReadShort();
            if (iconId < 0)
            {
                throw new System.Exception("Forbidden value (" + iconId + ") on element of IconNamedPreset.iconId.");
            }

            name = (string)reader.ReadUTF();
        }


    }
}








