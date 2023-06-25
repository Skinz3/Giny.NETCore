using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeStartOkRecycleTradeMessage : NetworkMessage  
    { 
        public  const ushort Id = 6635;
        public override ushort MessageId => Id;

        public short percentToPrism;
        public short percentToPlayer;

        public ExchangeStartOkRecycleTradeMessage()
        {
        }
        public ExchangeStartOkRecycleTradeMessage(short percentToPrism,short percentToPlayer)
        {
            this.percentToPrism = percentToPrism;
            this.percentToPlayer = percentToPlayer;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (percentToPrism < 0)
            {
                throw new System.Exception("Forbidden value (" + percentToPrism + ") on element percentToPrism.");
            }

            writer.WriteShort((short)percentToPrism);
            if (percentToPlayer < 0)
            {
                throw new System.Exception("Forbidden value (" + percentToPlayer + ") on element percentToPlayer.");
            }

            writer.WriteShort((short)percentToPlayer);
        }
        public override void Deserialize(IDataReader reader)
        {
            percentToPrism = (short)reader.ReadShort();
            if (percentToPrism < 0)
            {
                throw new System.Exception("Forbidden value (" + percentToPrism + ") on element of ExchangeStartOkRecycleTradeMessage.percentToPrism.");
            }

            percentToPlayer = (short)reader.ReadShort();
            if (percentToPlayer < 0)
            {
                throw new System.Exception("Forbidden value (" + percentToPlayer + ") on element of ExchangeStartOkRecycleTradeMessage.percentToPlayer.");
            }

        }


    }
}








