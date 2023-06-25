using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTeamMemberWithAllianceCharacterInformations : FightTeamMemberCharacterInformations  
    { 
        public new const ushort Id = 1452;
        public override ushort TypeId => Id;

        public BasicAllianceInformations allianceInfos;

        public FightTeamMemberWithAllianceCharacterInformations()
        {
        }
        public FightTeamMemberWithAllianceCharacterInformations(BasicAllianceInformations allianceInfos,double id,string name,short level)
        {
            this.allianceInfos = allianceInfos;
            this.id = id;
            this.name = name;
            this.level = level;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInfos.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInfos = new BasicAllianceInformations();
            allianceInfos.Deserialize(reader);
        }


    }
}








