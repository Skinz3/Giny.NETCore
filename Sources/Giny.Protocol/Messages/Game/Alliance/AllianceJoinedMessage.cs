using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceJoinedMessage : NetworkMessage
    {
        public const ushort Id = 6952;
        public override ushort MessageId => Id;

        public AllianceInformation allianceInfo;
        public int rankId;

        public AllianceJoinedMessage()
        {
        }
        public AllianceJoinedMessage(AllianceInformation allianceInfo, int rankId)
        {
            this.allianceInfo = allianceInfo;
            this.rankId = rankId;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceInfo.Serialize(writer);
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element rankId.");
            }

            writer.WriteVarInt((int)rankId);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceInfo = new AllianceInformation();
            allianceInfo.Deserialize(reader);
            rankId = (int)reader.ReadVarUhInt();
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element of AllianceJoinedMessage.rankId.");
            }

        }

    }
}


