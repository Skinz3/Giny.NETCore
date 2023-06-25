using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PaddockSellRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8060;
        public override ushort MessageId => Id;

        public long price;
        public bool forSale;

        public PaddockSellRequestMessage()
        {
        }
        public PaddockSellRequestMessage(long price,bool forSale)
        {
            this.price = price;
            this.forSale = forSale;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
            writer.WriteBoolean((bool)forSale);
        }
        public override void Deserialize(IDataReader reader)
        {
            price = (long)reader.ReadVarUhLong();
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of PaddockSellRequestMessage.price.");
            }

            forSale = (bool)reader.ReadBoolean();
        }


    }
}








