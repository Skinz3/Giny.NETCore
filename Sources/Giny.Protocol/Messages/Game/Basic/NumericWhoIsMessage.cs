using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class NumericWhoIsMessage : NetworkMessage
    {
        public const ushort Id = 5477;
        public override ushort MessageId => Id;

        public long playerId;
        public int accountId;

        public NumericWhoIsMessage()
        {
        }
        public NumericWhoIsMessage(long playerId, int accountId)
        {
            this.playerId = playerId;
            this.accountId = accountId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element accountId.");
            }

            writer.WriteInt((int)accountId);
        }
        public override void Deserialize(IDataReader reader)
        {
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of NumericWhoIsMessage.playerId.");
            }

            accountId = (int)reader.ReadInt();
            if (accountId < 0)
            {
                throw new System.Exception("Forbidden value (" + accountId + ") on element of NumericWhoIsMessage.accountId.");
            }

        }

    }
}


