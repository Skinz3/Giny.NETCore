using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeMoneyMovementInformationMessage : NetworkMessage
    {
        public const ushort Id = 5589;
        public override ushort MessageId => Id;

        public long limit;

        public ExchangeMoneyMovementInformationMessage()
        {
        }
        public ExchangeMoneyMovementInformationMessage(long limit)
        {
            this.limit = limit;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (limit < 0 || limit > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + limit + ") on element limit.");
            }

            writer.WriteVarLong((long)limit);
        }
        public override void Deserialize(IDataReader reader)
        {
            limit = (long)reader.ReadVarUhLong();
            if (limit < 0 || limit > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + limit + ") on element of ExchangeMoneyMovementInformationMessage.limit.");
            }

        }

    }
}


