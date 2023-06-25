using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobAllowMultiCraftRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 6676;
        public override ushort MessageId => Id;

        public bool enabled;

        public JobAllowMultiCraftRequestMessage()
        {
        }
        public JobAllowMultiCraftRequestMessage(bool enabled)
        {
            this.enabled = enabled;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)enabled);
        }
        public override void Deserialize(IDataReader reader)
        {
            enabled = (bool)reader.ReadBoolean();
        }


    }
}








