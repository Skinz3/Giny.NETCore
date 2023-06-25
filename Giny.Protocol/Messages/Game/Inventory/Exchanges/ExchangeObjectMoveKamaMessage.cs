using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeObjectMoveKamaMessage : NetworkMessage  
    { 
        public  const ushort Id = 200;
        public override ushort MessageId => Id;

        public long quantity;

        public ExchangeObjectMoveKamaMessage()
        {
        }
        public ExchangeObjectMoveKamaMessage(long quantity)
        {
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (quantity < -9.00719925474099E+15 || quantity > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarLong((long)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            quantity = (long)reader.ReadVarLong();
            if (quantity < -9.00719925474099E+15 || quantity > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ExchangeObjectMoveKamaMessage.quantity.");
            }

        }


    }
}








