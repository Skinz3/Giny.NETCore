using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildFactSheetInformations : GuildInformations  
    { 
        public new const ushort Id = 9183;
        public override ushort TypeId => Id;

        public long leaderId;
        public short nbMembers;
        public short lastActivityDay;
        public GuildRecruitmentInformation recruitment;
        public int nbPendingApply;

        public GuildFactSheetInformations()
        {
        }
        public GuildFactSheetInformations(long leaderId,short nbMembers,short lastActivityDay,GuildRecruitmentInformation recruitment,int nbPendingApply,int guildId,string guildName,byte guildLevel,GuildEmblem guildEmblem)
        {
            this.leaderId = leaderId;
            this.nbMembers = nbMembers;
            this.lastActivityDay = lastActivityDay;
            this.recruitment = recruitment;
            this.nbPendingApply = nbPendingApply;
            this.guildId = guildId;
            this.guildName = guildName;
            this.guildLevel = guildLevel;
            this.guildEmblem = guildEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (leaderId < 0 || leaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderId + ") on element leaderId.");
            }

            writer.WriteVarLong((long)leaderId);
            if (nbMembers < 0)
            {
                throw new System.Exception("Forbidden value (" + nbMembers + ") on element nbMembers.");
            }

            writer.WriteVarShort((short)nbMembers);
            if (lastActivityDay < 0)
            {
                throw new System.Exception("Forbidden value (" + lastActivityDay + ") on element lastActivityDay.");
            }

            writer.WriteShort((short)lastActivityDay);
            recruitment.Serialize(writer);
            if (nbPendingApply < 0)
            {
                throw new System.Exception("Forbidden value (" + nbPendingApply + ") on element nbPendingApply.");
            }

            writer.WriteInt((int)nbPendingApply);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            leaderId = (long)reader.ReadVarUhLong();
            if (leaderId < 0 || leaderId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + leaderId + ") on element of GuildFactSheetInformations.leaderId.");
            }

            nbMembers = (short)reader.ReadVarUhShort();
            if (nbMembers < 0)
            {
                throw new System.Exception("Forbidden value (" + nbMembers + ") on element of GuildFactSheetInformations.nbMembers.");
            }

            lastActivityDay = (short)reader.ReadShort();
            if (lastActivityDay < 0)
            {
                throw new System.Exception("Forbidden value (" + lastActivityDay + ") on element of GuildFactSheetInformations.lastActivityDay.");
            }

            recruitment = new GuildRecruitmentInformation();
            recruitment.Deserialize(reader);
            nbPendingApply = (int)reader.ReadInt();
            if (nbPendingApply < 0)
            {
                throw new System.Exception("Forbidden value (" + nbPendingApply + ") on element of GuildFactSheetInformations.nbPendingApply.");
            }

        }


    }
}








