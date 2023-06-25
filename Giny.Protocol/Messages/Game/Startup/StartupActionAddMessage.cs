using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StartupActionAddMessage : NetworkMessage  
    { 
        public  const ushort Id = 9405;
        public override ushort MessageId => Id;

        public StartupActionAddObject newAction;

        public StartupActionAddMessage()
        {
        }
        public StartupActionAddMessage(StartupActionAddObject newAction)
        {
            this.newAction = newAction;
        }
        public override void Serialize(IDataWriter writer)
        {
            newAction.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            newAction = new StartupActionAddObject();
            newAction.Deserialize(reader);
        }


    }
}








