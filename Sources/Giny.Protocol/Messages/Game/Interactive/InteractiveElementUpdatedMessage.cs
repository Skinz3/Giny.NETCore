using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InteractiveElementUpdatedMessage : NetworkMessage  
    { 
        public  const ushort Id = 322;
        public override ushort MessageId => Id;

        public InteractiveElement interactiveElement;

        public InteractiveElementUpdatedMessage()
        {
        }
        public InteractiveElementUpdatedMessage(InteractiveElement interactiveElement)
        {
            this.interactiveElement = interactiveElement;
        }
        public override void Serialize(IDataWriter writer)
        {
            interactiveElement.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            interactiveElement = new InteractiveElement();
            interactiveElement.Deserialize(reader);
        }


    }
}








