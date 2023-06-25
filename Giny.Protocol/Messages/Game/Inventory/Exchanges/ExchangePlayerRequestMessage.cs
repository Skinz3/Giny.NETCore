using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangePlayerRequestMessage : ExchangeRequestMessage  
    { 
        public new const ushort Id = 4119;
        public override ushort MessageId => Id;

        public long target;

        public ExchangePlayerRequestMessage()
        {
        }
        public ExchangePlayerRequestMessage(long target,byte exchangeType)
        {
            this.target = target;
            this.exchangeType = exchangeType;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (target < 0 || target > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + target + ") on element target.");
            }

            writer.WriteVarLong((long)target);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            target = (long)reader.ReadVarUhLong();
            if (target < 0 || target > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + target + ") on element of ExchangePlayerRequestMessage.target.");
            }

        }


    }
}








