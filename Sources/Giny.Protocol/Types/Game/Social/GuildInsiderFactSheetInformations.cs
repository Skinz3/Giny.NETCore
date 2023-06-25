using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildInsiderFactSheetInformations : GuildFactSheetInformations  
    { 
        public new const ushort Id = 5698;
        public override ushort TypeId => Id;

        public string leaderName;
        public short nbConnectedMembers;
        public byte nbTaxCollectors;

        public GuildInsiderFactSheetInformations()
        {
        }
        public GuildInsiderFactSheetInformations(string leaderName,short nbConnectedMembers,byte nbTaxCollectors,int guildId,string guildName,byte guildLevel,GuildEmblem guildEmblem,long leaderId,short nbMembers,short lastActivityDay,GuildRecruitmentInformation recruitment,int nbPendingApply)
        {
            this.leaderName = leaderName;
            this.nbConnectedMembers = nbConnectedMembers;
            this.nbTaxCollectors = nbTaxCollectors;
            this.guildId = guildId;
            this.guildName = guildName;
            this.guildLevel = guildLevel;
            this.guildEmblem = guildEmblem;
            this.leaderId = leaderId;
            this.nbMembers = nbMembers;
            this.lastActivityDay = lastActivityDay;
            this.recruitment = recruitment;
            this.nbPendingApply = nbPendingApply;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)leaderName);
            if (nbConnectedMembers < 0)
            {
                throw new System.Exception("Forbidden value (" + nbConnectedMembers + ") on element nbConnectedMembers.");
            }

            writer.WriteVarShort((short)nbConnectedMembers);
            if (nbTaxCollectors < 0)
            {
                throw new System.Exception("Forbidden value (" + nbTaxCollectors + ") on element nbTaxCollectors.");
            }

            writer.WriteByte((byte)nbTaxCollectors);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            leaderName = (string)reader.ReadUTF();
            nbConnectedMembers = (short)reader.ReadVarUhShort();
            if (nbConnectedMembers < 0)
            {
                throw new System.Exception("Forbidden value (" + nbConnectedMembers + ") on element of GuildInsiderFactSheetInformations.nbConnectedMembers.");
            }

            nbTaxCollectors = (byte)reader.ReadByte();
            if (nbTaxCollectors < 0)
            {
                throw new System.Exception("Forbidden value (" + nbTaxCollectors + ") on element of GuildInsiderFactSheetInformations.nbTaxCollectors.");
            }

        }


    }
}








