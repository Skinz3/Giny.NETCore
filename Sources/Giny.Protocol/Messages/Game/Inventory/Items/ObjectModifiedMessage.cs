using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectModifiedMessage : NetworkMessage  
    { 
        public  const ushort Id = 829;
        public override ushort MessageId => Id;

        public ObjectItem @object;

        public ObjectModifiedMessage()
        {
        }
        public ObjectModifiedMessage(ObjectItem @object)
        {
            this.@object = @object;
        }
        public override void Serialize(IDataWriter writer)
        {
            @object.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            @object = new ObjectItem();
            @object.Deserialize(reader);
        }


    }
}








