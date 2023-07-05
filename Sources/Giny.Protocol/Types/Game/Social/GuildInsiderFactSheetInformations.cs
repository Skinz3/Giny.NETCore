using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GuildInsiderFactSheetInformations : GuildFactSheetInformations
    {
        public new const ushort Id = 1142;
        public override ushort TypeId => Id;

        public string leaderName;

        public GuildInsiderFactSheetInformations()
        {
        }
        public GuildInsiderFactSheetInformations(string leaderName, int guildId, string guildName, byte guildLevel, SocialEmblem guildEmblem, long leaderId, short nbMembers, short lastActivityDay, GuildRecruitmentInformation recruitment, int nbPendingApply)
        {
            this.leaderName = leaderName;
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
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            leaderName = (string)reader.ReadUTF();
        }


    }
}


