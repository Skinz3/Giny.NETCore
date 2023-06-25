using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidPriceMessage : NetworkMessage  
    { 
        public  const ushort Id = 4385;
        public override ushort MessageId => Id;

        public short genericId;
        public long averagePrice;

        public ExchangeBidPriceMessage()
        {
        }
        public ExchangeBidPriceMessage(short genericId,long averagePrice)
        {
            this.genericId = genericId;
            this.averagePrice = averagePrice;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (genericId < 0)
            {
                throw new System.Exception("Forbidden value (" + genericId + ") on element genericId.");
            }

            writer.WriteVarShort((short)genericId);
            if (averagePrice < -9.00719925474099E+15 || averagePrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + averagePrice + ") on element averagePrice.");
            }

            writer.WriteVarLong((long)averagePrice);
        }
        public override void Deserialize(IDataReader reader)
        {
            genericId = (short)reader.ReadVarUhShort();
            if (genericId < 0)
            {
                throw new System.Exception("Forbidden value (" + genericId + ") on element of ExchangeBidPriceMessage.genericId.");
            }

            averagePrice = (long)reader.ReadVarLong();
            if (averagePrice < -9.00719925474099E+15 || averagePrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + averagePrice + ") on element of ExchangeBidPriceMessage.averagePrice.");
            }

        }


    }
}








