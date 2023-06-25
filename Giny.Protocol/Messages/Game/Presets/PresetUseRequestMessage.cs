using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PresetUseRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8428;
        public override ushort MessageId => Id;

        public short presetId;

        public PresetUseRequestMessage()
        {
        }
        public PresetUseRequestMessage(short presetId)
        {
            this.presetId = presetId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)presetId);
        }
        public override void Deserialize(IDataReader reader)
        {
            presetId = (short)reader.ReadShort();
        }


    }
}








