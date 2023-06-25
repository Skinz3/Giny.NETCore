using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectQuantityMessage : NetworkMessage  
    { 
        public  const ushort Id = 7075;
        public override ushort MessageId => Id;

        public int objectUID;
        public int quantity;
        public byte origin;

        public ObjectQuantityMessage()
        {
        }
        public ObjectQuantityMessage(int objectUID,int quantity,byte origin)
        {
            this.objectUID = objectUID;
            this.quantity = quantity;
            this.origin = origin;
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
            writer.WriteByte((byte)origin);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectQuantityMessage.objectUID.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ObjectQuantityMessage.quantity.");
            }

            origin = (byte)reader.ReadByte();
            if (origin < 0)
            {
                throw new System.Exception("Forbidden value (" + origin + ") on element of ObjectQuantityMessage.origin.");
            }

        }


    }
}








