using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class WarnOnPermaDeathMessage : NetworkMessage  
    { 
        public  const ushort Id = 3252;
        public override ushort MessageId => Id;

        public bool enable;

        public WarnOnPermaDeathMessage()
        {
        }
        public WarnOnPermaDeathMessage(bool enable)
        {
            this.enable = enable;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)enable);
        }
        public override void Deserialize(IDataReader reader)
        {
            enable = (bool)reader.ReadBoolean();
        }


    }
}








