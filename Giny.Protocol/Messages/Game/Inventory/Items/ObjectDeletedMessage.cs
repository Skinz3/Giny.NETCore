using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectDeletedMessage : NetworkMessage  
    { 
        public  const ushort Id = 6390;
        public override ushort MessageId => Id;

        public int objectUID;

        public ObjectDeletedMessage()
        {
        }
        public ObjectDeletedMessage(int objectUID)
        {
            this.objectUID = objectUID;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectDeletedMessage.objectUID.");
            }

        }


    }
}








