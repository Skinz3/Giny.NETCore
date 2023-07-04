using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeStartOkNpcTradeMessage : NetworkMessage
    {
        public const ushort Id = 6958;
        public override ushort MessageId => Id;

        public double npcId;

        public ExchangeStartOkNpcTradeMessage()
        {
        }
        public ExchangeStartOkNpcTradeMessage(double npcId)
        {
            this.npcId = npcId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (npcId < -9007199254740992 || npcId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + npcId + ") on element npcId.");
            }

            writer.WriteDouble((double)npcId);
        }
        public override void Deserialize(IDataReader reader)
        {
            npcId = (double)reader.ReadDouble();
            if (npcId < -9007199254740992 || npcId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + npcId + ") on element of ExchangeStartOkNpcTradeMessage.npcId.");
            }

        }

    }
}


