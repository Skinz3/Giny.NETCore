using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EntitiesInformationMessage : NetworkMessage  
    { 
        public  const ushort Id = 4126;
        public override ushort MessageId => Id;

        public EntityInformation[] entities;

        public EntitiesInformationMessage()
        {
        }
        public EntitiesInformationMessage(EntityInformation[] entities)
        {
            this.entities = entities;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)entities.Length);
            for (uint _i1 = 0;_i1 < entities.Length;_i1++)
            {
                (entities[_i1] as EntityInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            EntityInformation _item1 = null;
            uint _entitiesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _entitiesLen;_i1++)
            {
                _item1 = new EntityInformation();
                _item1.Deserialize(reader);
                entities[_i1] = _item1;
            }

        }


    }
}








