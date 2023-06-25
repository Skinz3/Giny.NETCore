using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectMovePricedMessage : ExchangeObjectMoveMessage  
    { 
        public new const ushort Id = 3046;
        public override ushort MessageId => Id;

        public long price;

        public ExchangeObjectMovePricedMessage()
        {
        }
        public ExchangeObjectMovePricedMessage(long price,int objectUID,int quantity)
        {
            this.price = price;
            this.objectUID = objectUID;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            price = (long)reader.ReadVarUhLong();
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of ExchangeObjectMovePricedMessage.price.");
            }

        }


    }
}








