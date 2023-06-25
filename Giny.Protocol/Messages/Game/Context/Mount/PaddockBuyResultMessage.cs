using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PaddockBuyResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 3573;
        public override ushort MessageId => Id;

        public double paddockId;
        public bool bought;
        public long realPrice;

        public PaddockBuyResultMessage()
        {
        }
        public PaddockBuyResultMessage(double paddockId,bool bought,long realPrice)
        {
            this.paddockId = paddockId;
            this.bought = bought;
            this.realPrice = realPrice;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (paddockId < 0 || paddockId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + paddockId + ") on element paddockId.");
            }

            writer.WriteDouble((double)paddockId);
            writer.WriteBoolean((bool)bought);
            if (realPrice < 0 || realPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + realPrice + ") on element realPrice.");
            }

            writer.WriteVarLong((long)realPrice);
        }
        public override void Deserialize(IDataReader reader)
        {
            paddockId = (double)reader.ReadDouble();
            if (paddockId < 0 || paddockId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + paddockId + ") on element of PaddockBuyResultMessage.paddockId.");
            }

            bought = (bool)reader.ReadBoolean();
            realPrice = (long)reader.ReadVarUhLong();
            if (realPrice < 0 || realPrice > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + realPrice + ") on element of PaddockBuyResultMessage.realPrice.");
            }

        }


    }
}








