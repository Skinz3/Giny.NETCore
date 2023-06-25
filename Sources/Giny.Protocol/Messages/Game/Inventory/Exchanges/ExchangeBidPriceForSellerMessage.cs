using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidPriceForSellerMessage : ExchangeBidPriceMessage  
    { 
        public new const ushort Id = 2252;
        public override ushort MessageId => Id;

        public bool allIdentical;
        public long[] minimalPrices;

        public ExchangeBidPriceForSellerMessage()
        {
        }
        public ExchangeBidPriceForSellerMessage(bool allIdentical,long[] minimalPrices,short genericId,long averagePrice)
        {
            this.allIdentical = allIdentical;
            this.minimalPrices = minimalPrices;
            this.genericId = genericId;
            this.averagePrice = averagePrice;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)allIdentical);
            writer.WriteShort((short)minimalPrices.Length);
            for (uint _i2 = 0;_i2 < minimalPrices.Length;_i2++)
            {
                if (minimalPrices[_i2] < 0 || minimalPrices[_i2] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + minimalPrices[_i2] + ") on element 2 (starting at 1) of minimalPrices.");
                }

                writer.WriteVarLong((long)minimalPrices[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val2 = double.NaN;
            base.Deserialize(reader);
            allIdentical = (bool)reader.ReadBoolean();
            uint _minimalPricesLen = (uint)reader.ReadUShort();
            minimalPrices = new long[_minimalPricesLen];
            for (uint _i2 = 0;_i2 < _minimalPricesLen;_i2++)
            {
                _val2 = (double)reader.ReadVarUhLong();
                if (_val2 < 0 || _val2 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of minimalPrices.");
                }

                minimalPrices[_i2] = (long)_val2;
            }

        }


    }
}








