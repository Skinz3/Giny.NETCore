using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachExitResponseMessage : NetworkMessage  
    { 
        public  const ushort Id = 1265;
        public override ushort MessageId => Id;

        public bool exited;

        public BreachExitResponseMessage()
        {
        }
        public BreachExitResponseMessage(bool exited)
        {
            this.exited = exited;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)exited);
        }
        public override void Deserialize(IDataReader reader)
        {
            exited = (bool)reader.ReadBoolean();
        }


    }
}








