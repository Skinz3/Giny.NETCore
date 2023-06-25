using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PaddockSellBuyDialogMessage : NetworkMessage  
    { 
        public  const ushort Id = 375;
        public override ushort MessageId => Id;

        public bool bsell;
        public int ownerId;
        public long price;

        public PaddockSellBuyDialogMessage()
        {
        }
        public PaddockSellBuyDialogMessage(bool bsell,int ownerId,long price)
        {
            this.bsell = bsell;
            this.ownerId = ownerId;
            this.price = price;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)bsell);
            if (ownerId < 0)
            {
                throw new System.Exception("Forbidden value (" + ownerId + ") on element ownerId.");
            }

            writer.WriteVarInt((int)ownerId);
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element price.");
            }

            writer.WriteVarLong((long)price);
        }
        public override void Deserialize(IDataReader reader)
        {
            bsell = (bool)reader.ReadBoolean();
            ownerId = (int)reader.ReadVarUhInt();
            if (ownerId < 0)
            {
                throw new System.Exception("Forbidden value (" + ownerId + ") on element of PaddockSellBuyDialogMessage.ownerId.");
            }

            price = (long)reader.ReadVarUhLong();
            if (price < 0 || price > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + price + ") on element of PaddockSellBuyDialogMessage.price.");
            }

        }


    }
}








