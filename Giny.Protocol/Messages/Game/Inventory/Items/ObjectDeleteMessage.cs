using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectDeleteMessage : NetworkMessage  
    { 
        public  const ushort Id = 3038;
        public override ushort MessageId => Id;

        public int objectUID;
        public int quantity;

        public ObjectDeleteMessage()
        {
        }
        public ObjectDeleteMessage(int objectUID,int quantity)
        {
            this.objectUID = objectUID;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarInt((int)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectDeleteMessage.objectUID.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ObjectDeleteMessage.quantity.");
            }

        }


    }
}








