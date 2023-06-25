using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartOkHumanVendorMessage : NetworkMessage  
    { 
        public  const ushort Id = 627;
        public override ushort MessageId => Id;

        public double sellerId;
        public ObjectItemToSellInHumanVendorShop[] objectsInfos;

        public ExchangeStartOkHumanVendorMessage()
        {
        }
        public ExchangeStartOkHumanVendorMessage(double sellerId,ObjectItemToSellInHumanVendorShop[] objectsInfos)
        {
            this.sellerId = sellerId;
            this.objectsInfos = objectsInfos;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (sellerId < -9.00719925474099E+15 || sellerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sellerId + ") on element sellerId.");
            }

            writer.WriteDouble((double)sellerId);
            writer.WriteShort((short)objectsInfos.Length);
            for (uint _i2 = 0;_i2 < objectsInfos.Length;_i2++)
            {
                (objectsInfos[_i2] as ObjectItemToSellInHumanVendorShop).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItemToSellInHumanVendorShop _item2 = null;
            sellerId = (double)reader.ReadDouble();
            if (sellerId < -9.00719925474099E+15 || sellerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + sellerId + ") on element of ExchangeStartOkHumanVendorMessage.sellerId.");
            }

            uint _objectsInfosLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _objectsInfosLen;_i2++)
            {
                _item2 = new ObjectItemToSellInHumanVendorShop();
                _item2.Deserialize(reader);
                objectsInfos[_i2] = _item2;
            }

        }


    }
}








