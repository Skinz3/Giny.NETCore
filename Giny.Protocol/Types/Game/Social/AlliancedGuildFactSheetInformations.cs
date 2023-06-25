using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AlliancedGuildFactSheetInformations : GuildInformations  
    { 
        public new const ushort Id = 7484;
        public override ushort TypeId => Id;

        public BasicNamedAllianceInformations allianceInfos;

        public AlliancedGuildFactSheetInformations()
        {
        }
        public AlliancedGuildFactSheetInformations(BasicNamedAllianceInformations allianceInfos,int guildId,string guildName,byte guildLevel,GuildEmblem guildEmblem)
        {
            this.allianceInfos = allianceInfos;
            this.guildId = guildId;
            this.guildName = guildName;
            this.guildLevel = guildLevel;
            this.guildEmblem = guildEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInfos.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInfos = new BasicNamedAllianceInformations();
            allianceInfos.Deserialize(reader);
        }


    }
}








