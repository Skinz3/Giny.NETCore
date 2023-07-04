using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceChangeMemberRankMessage : NetworkMessage
    {
        public const ushort Id = 2507;
        public override ushort MessageId => Id;

        public long memberId;
        public int rankId;

        public AllianceChangeMemberRankMessage()
        {
        }
        public AllianceChangeMemberRankMessage(long memberId, int rankId)
        {
            this.memberId = memberId;
            this.rankId = rankId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (memberId < 0 || memberId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element rankId.");
            }

            writer.WriteVarInt((int)rankId);
        }
        public override void Deserialize(IDataReader reader)
        {
            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of AllianceChangeMemberRankMessage.memberId.");
            }

            rankId = (int)reader.ReadVarUhInt();
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element of AllianceChangeMemberRankMessage.rankId.");
            }

        }

    }
}


