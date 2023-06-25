using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectMoveMessage : NetworkMessage  
    { 
        public  const ushort Id = 150;
        public override ushort MessageId => Id;

        public int objectUID;
        public int quantity;

        public ExchangeObjectMoveMessage()
        {
        }
        public ExchangeObjectMoveMessage(int objectUID,int quantity)
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
            writer.WriteVarInt((int)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectUID = (int)reader.ReadVarUhInt();
            if (objectUID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectUID + ") on element of ExchangeObjectMoveMessage.objectUID.");
            }

            quantity = (int)reader.ReadVarInt();
        }


    }
}








