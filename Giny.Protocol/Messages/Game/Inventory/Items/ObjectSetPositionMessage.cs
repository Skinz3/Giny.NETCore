using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ObjectSetPositionMessage : NetworkMessage  
    { 
        public  const ushort Id = 1022;
        public override ushort MessageId => Id;

        public int objectUID;
        public short position;
        public int quantity;

        public ObjectSetPositionMessage()
        {
        }
        public ObjectSetPositionMessage(int objectUID,short position,int quantity)
        {
            this.objectUID = objectUID;
            this.position = position;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element objectUID.");
            }

            writer.WriteVarInt((int)objectUID);
            writer.WriteShort((short)position);
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
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ObjectSetPositionMessage.objectUID.");
            }

            position = (short)reader.ReadShort();
            if (position < 0)
            {
                throw new System.Exception("Forbidden value (" + position + ") on element of ObjectSetPositionMessage.position.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ObjectSetPositionMessage.quantity.");
            }

        }


    }
}








