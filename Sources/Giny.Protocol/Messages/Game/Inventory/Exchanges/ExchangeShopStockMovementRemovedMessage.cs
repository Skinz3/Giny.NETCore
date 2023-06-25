using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeShopStockMovementRemovedMessage : NetworkMessage  
    { 
        public  const ushort Id = 7314;
        public override ushort MessageId => Id;

        public int objectId;

        public ExchangeShopStockMovementRemovedMessage()
        {
        }
        public ExchangeShopStockMovementRemovedMessage(int objectId)
        {
            this.objectId = objectId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectId + ") on element objectId.");
            }

            writer.WriteVarInt((int)objectId);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectId = (int)reader.ReadVarUhInt();
            if (objectId < 0)
            {
                throw new System.Exception("Forbidden value (" + objectId + ") on element of ExchangeShopStockMovementRemovedMessage.objectId.");
            }

        }


    }
}








