using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeBidHouseBuyMessage : NetworkMessage  
    { 
        public  const ushort Id = 6750;
        public override ushort MessageId => Id;

        public int uid;
        public int qty;
        public long price;

        public ExchangeBidHouseBuyMessage()
        {
        }
        public ExchangeBidHouseBuyMessage(int uid,int qty,long price)
        {
            this.uid = uid;
            this.qty = qty;
            this.price = price;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element uid.");
            }

            writer.WriteVarInt((int)uid);
            if (qty < 0)
            {
                throw new System.Exception("Forbidden value (" + qty + ") on element qty.");
            }

            writer.WriteVarInt((int)qty);
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
        }
        public override void Deserialize(IDataReader reader)
        {
            uid = (int)reader.ReadVarUhInt();
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element of ExchangeBidHouseBuyMessage.uid.");
            }

            qty = (int)reader.ReadVarUhInt();
            if (qty < 0)
            {
                throw new System.Exception("Forbidden value (" + qty + ") on element of ExchangeBidHouseBuyMessage.qty.");
            }

            price = (long)reader.ReadVarUhLong();
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of ExchangeBidHouseBuyMessage.price.");
            }

        }


    }
}








