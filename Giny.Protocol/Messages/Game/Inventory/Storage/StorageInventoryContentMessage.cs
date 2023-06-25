using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StorageInventoryContentMessage : InventoryContentMessage  
    { 
        public new const ushort Id = 7983;
        public override ushort MessageId => Id;


        public StorageInventoryContentMessage()
        {
        }
        public StorageInventoryContentMessage(ObjectItem[] objects,long kamas)
        {
            this.objects = objects;
            this.kamas = kamas;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








