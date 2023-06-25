using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeRequestedTradeMessage : ExchangeRequestedMessage  
    { 
        public new const ushort Id = 6276;
        public override ushort MessageId => Id;

        public long source;
        public long target;

        public ExchangeRequestedTradeMessage()
        {
        }
        public ExchangeRequestedTradeMessage(long source,long target,byte exchangeType)
        {
            this.source = source;
            this.target = target;
            this.exchangeType = exchangeType;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (source < 0 || source > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + source + ") on element source.");
            }

            writer.WriteVarLong((long)source);
            if (target < 0 || target > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + target + ") on element target.");
            }

            writer.WriteVarLong((long)target);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            source = (long)reader.ReadVarUhLong();
            if (source < 0 || source > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + source + ") on element of ExchangeRequestedTradeMessage.source.");
            }

            target = (long)reader.ReadVarUhLong();
            if (target < 0 || target > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + target + ") on element of ExchangeRequestedTradeMessage.target.");
            }

        }


    }
}








