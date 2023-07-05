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
        public const ushort Id = 9764;
        public override ushort MessageId => Id;

        public int genericId;
        public long averagePrice;

        public ExchangeBidPriceMessage()
        {
        }
        public ExchangeBidPriceMessage(int genericId, long averagePrice)
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

            writer.WriteVarInt((int)genericId);
            if (averagePrice < -9007199254740992 || averagePrice > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + averagePrice + ") on element averagePrice.");
            }

            writer.WriteVarLong((long)averagePrice);
        }
        public override void Deserialize(IDataReader reader)
        {
            genericId = (int)reader.ReadVarUhInt();
            if (genericId < 0)
            {
                throw new System.Exception("Forbidden value (" + genericId + ") on element of ExchangeBidPriceMessage.genericId.");
            }

            averagePrice = (long)reader.ReadVarLong();
            if (averagePrice < -9007199254740992 || averagePrice > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + averagePrice + ") on element of ExchangeBidPriceMessage.averagePrice.");
            }

        }

    }
}


