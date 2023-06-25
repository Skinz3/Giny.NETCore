using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PresetDeleteRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1661;
        public override ushort MessageId => Id;

        public short presetId;

        public PresetDeleteRequestMessage()
        {
        }
        public PresetDeleteRequestMessage(short presetId)
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








