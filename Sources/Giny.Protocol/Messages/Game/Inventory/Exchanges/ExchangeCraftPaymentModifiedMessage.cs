using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeCraftPaymentModifiedMessage : NetworkMessage  
    { 
        public  const ushort Id = 9955;
        public override ushort MessageId => Id;

        public long goldSum;

        public ExchangeCraftPaymentModifiedMessage()
        {
        }
        public ExchangeCraftPaymentModifiedMessage(long goldSum)
        {
            this.goldSum = goldSum;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (goldSum < 0 || goldSum > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + goldSum + ") on element goldSum.");
            }

            writer.WriteVarLong((long)goldSum);
        }
        public override void Deserialize(IDataReader reader)
        {
            goldSum = (long)reader.ReadVarUhLong();
            if (goldSum < 0 || goldSum > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + goldSum + ") on element of ExchangeCraftPaymentModifiedMessage.goldSum.");
            }

        }


    }
}








