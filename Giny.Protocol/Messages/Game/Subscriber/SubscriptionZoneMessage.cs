using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SubscriptionZoneMessage : NetworkMessage  
    { 
        public  const ushort Id = 5068;
        public override ushort MessageId => Id;

        public bool active;

        public SubscriptionZoneMessage()
        {
        }
        public SubscriptionZoneMessage(bool active)
        {
            this.active = active;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)active);
        }
        public override void Deserialize(IDataReader reader)
        {
            active = (bool)reader.ReadBoolean();
        }


    }
}








