using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachTeleportResponseMessage : NetworkMessage  
    { 
        public  const ushort Id = 2050;
        public override ushort MessageId => Id;

        public bool teleported;

        public BreachTeleportResponseMessage()
        {
        }
        public BreachTeleportResponseMessage(bool teleported)
        {
            this.teleported = teleported;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)teleported);
        }
        public override void Deserialize(IDataReader reader)
        {
            teleported = (bool)reader.ReadBoolean();
        }


    }
}








