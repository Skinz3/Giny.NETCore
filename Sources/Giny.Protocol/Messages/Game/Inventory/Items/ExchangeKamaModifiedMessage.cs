using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeKamaModifiedMessage : ExchangeObjectMessage  
    { 
        public new const ushort Id = 4572;
        public override ushort MessageId => Id;

        public long quantity;

        public ExchangeKamaModifiedMessage()
        {
        }
        public ExchangeKamaModifiedMessage(long quantity,bool remote)
        {
            this.quantity = quantity;
            this.remote = remote;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (quantity < 0 || quantity > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarLong((long)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            quantity = (long)reader.ReadVarUhLong();
            if (quantity < 0 || quantity > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of ExchangeKamaModifiedMessage.quantity.");
            }

        }


    }
}








