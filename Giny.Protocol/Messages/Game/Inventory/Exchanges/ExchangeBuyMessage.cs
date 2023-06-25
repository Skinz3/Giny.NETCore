using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBuyMessage : NetworkMessage  
    { 
        public  const ushort Id = 5063;
        public override ushort MessageId => Id;

        public int objectToBuyId;
        public int quantity;

        public ExchangeBuyMessage()
        {
        }
        public ExchangeBuyMessage(int objectToBuyId,int quantity)
        {
            this.objectToBuyId = objectToBuyId;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectToBuyId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectToBuyId + ") on element objectToBuyId.");
            }

            writer.WriteVarInt((int)objectToBuyId);
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarInt((int)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectToBuyId = (int)reader.ReadVarUhInt();
            if (objectToBuyId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectToBuyId + ") on element of ExchangeBuyMessage.objectToBuyId.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ExchangeBuyMessage.quantity.");
            }

        }


    }
}








