using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeSellMessage : NetworkMessage  
    { 
        public  const ushort Id = 2623;
        public override ushort MessageId => Id;

        public int objectToSellId;
        public int quantity;

        public ExchangeSellMessage()
        {
        }
        public ExchangeSellMessage(int objectToSellId,int quantity)
        {
            this.objectToSellId = objectToSellId;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectToSellId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectToSellId + ") on element objectToSellId.");
            }

            writer.WriteVarInt((int)objectToSellId);
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarInt((int)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectToSellId = (int)reader.ReadVarUhInt();
            if (objectToSellId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectToSellId + ") on element of ExchangeSellMessage.objectToSellId.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ExchangeSellMessage.quantity.");
            }

        }


    }
}








