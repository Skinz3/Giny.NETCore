using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EntityInformationMessage : NetworkMessage  
    { 
        public  const ushort Id = 6691;
        public override ushort MessageId => Id;

        public EntityInformation entity;

        public EntityInformationMessage()
        {
        }
        public EntityInformationMessage(EntityInformation entity)
        {
            this.entity = entity;
        }
        public override void Serialize(IDataWriter writer)
        {
            entity.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            entity = new EntityInformation();
            entity.Deserialize(reader);
        }


    }
}








