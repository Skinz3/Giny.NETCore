using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StorageObjectUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 6026;
        public override ushort MessageId => Id;

        public ObjectItem @object;

        public StorageObjectUpdateMessage()
        {
        }
        public StorageObjectUpdateMessage(ObjectItem @object)
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








