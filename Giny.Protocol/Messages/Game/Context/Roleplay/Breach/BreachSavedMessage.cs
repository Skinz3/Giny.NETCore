using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachSavedMessage : NetworkMessage  
    { 
        public  const ushort Id = 593;
        public override ushort MessageId => Id;

        public bool saved;

        public BreachSavedMessage()
        {
        }
        public BreachSavedMessage(bool saved)
        {
            this.saved = saved;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)saved);
        }
        public override void Deserialize(IDataReader reader)
        {
            saved = (bool)reader.ReadBoolean();
        }


    }
}








