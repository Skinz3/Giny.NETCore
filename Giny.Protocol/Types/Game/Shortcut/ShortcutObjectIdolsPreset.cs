using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class ShortcutObjectIdolsPreset : ShortcutObject  
    { 
        public new const ushort Id = 889;
        public override ushort TypeId => Id;

        public short presetId;

        public ShortcutObjectIdolsPreset()
        {
        }
        public ShortcutObjectIdolsPreset(short presetId,byte slot)
        {
            this.presetId = presetId;
            this.slot = slot;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)presetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            presetId = (short)reader.ReadShort();
        }


    }
}








