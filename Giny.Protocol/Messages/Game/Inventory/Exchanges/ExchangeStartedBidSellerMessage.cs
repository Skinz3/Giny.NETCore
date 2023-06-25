using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartedBidSellerMessage : NetworkMessage  
    { 
        public  const ushort Id = 2021;
        public override ushort MessageId => Id;

        public SellerBuyerDescriptor sellerDescriptor;
        public ObjectItemToSellInBid[] objectsInfos;

        public ExchangeStartedBidSellerMessage()
        {
        }
        public ExchangeStartedBidSellerMessage(SellerBuyerDescriptor sellerDescriptor,ObjectItemToSellInBid[] objectsInfos)
        {
            this.sellerDescriptor = sellerDescriptor;
            this.objectsInfos = objectsInfos;
        }
        public override void Serialize(IDataWriter writer)
        {
            sellerDescriptor.Serialize(writer);
            writer.WriteShort((short)objectsInfos.Length);
            for (uint _i2 = 0;_i2 < objectsInfos.Length;_i2++)
            {
                (objectsInfos[_i2] as ObjectItemToSellInBid).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemToSellInBid _item2 = null;
            sellerDescriptor = new SellerBuyerDescriptor();
            sellerDescriptor.Deserialize(reader);
            uint _objectsInfosLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _objectsInfosLen;_i2++)
            {
                _item2 = new ObjectItemToSellInBid();
                _item2.Deserialize(reader);
                objectsInfos[_i2] = _item2;
            }

        }


    }
}








