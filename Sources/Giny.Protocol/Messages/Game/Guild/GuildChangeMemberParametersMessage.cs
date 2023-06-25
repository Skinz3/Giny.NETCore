using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildChangeMemberParametersMessage : NetworkMessage  
    { 
        public  const ushort Id = 9008;
        public override ushort MessageId => Id;

        public long memberId;
        public int rankId;
        public byte experienceGivenPercent;

        public GuildChangeMemberParametersMessage()
        {
        }
        public GuildChangeMemberParametersMessage(long memberId,int rankId,byte experienceGivenPercent)
        {
            this.memberId = memberId;
            this.rankId = rankId;
            this.experienceGivenPercent = experienceGivenPercent;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element rankId.");
            }

            writer.WriteVarInt((int)rankId);
            if (experienceGivenPercent < 0 || experienceGivenPercent > 100)
            {
                throw new System.Exception("Forbidden value (" + experienceGivenPercent + ") on element experienceGivenPercent.");
            }

            writer.WriteByte((byte)experienceGivenPercent);
        }
        public override void Deserialize(IDataReader reader)
        {
            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of GuildChangeMemberParametersMessage.memberId.");
            }

            rankId = (int)reader.ReadVarUhInt();
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element of GuildChangeMemberParametersMessage.rankId.");
            }

            experienceGivenPercent = (byte)reader.ReadByte();
            if (experienceGivenPercent < 0 || experienceGivenPercent > 100)
            {
                throw new System.Exception("Forbidden value (" + experienceGivenPercent + ") on element of GuildChangeMemberParametersMessage.experienceGivenPercent.");
            }

        }


    }
}








