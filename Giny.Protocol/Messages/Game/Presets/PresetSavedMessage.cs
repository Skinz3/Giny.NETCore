using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PresetSavedMessage : NetworkMessage  
    { 
        public  const ushort Id = 8551;
        public override ushort MessageId => Id;

        public short presetId;
        public Preset preset;

        public PresetSavedMessage()
        {
        }
        public PresetSavedMessage(short presetId,Preset preset)
        {
            this.presetId = presetId;
            this.preset = preset;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)presetId);
            writer.WriteShort((short)preset.TypeId);
            preset.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            presetId = (short)reader.ReadShort();
            uint _id2 = (uint)reader.ReadUShort();
            preset = ProtocolTypeManager.GetInstance<Preset>((short)_id2);
            preset.Deserialize(reader);
        }


    }
}








