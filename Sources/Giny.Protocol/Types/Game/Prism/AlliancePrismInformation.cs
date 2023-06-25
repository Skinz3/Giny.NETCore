using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AlliancePrismInformation : PrismInformation  
    { 
        public new const ushort Id = 766;
        public override ushort TypeId => Id;

        public AllianceInformations alliance;

        public AlliancePrismInformation()
        {
        }
        public AlliancePrismInformation(AllianceInformations alliance,byte typeId,byte state,int nextVulnerabilityDate,int placementDate,int rewardTokenCount)
        {
            this.alliance = alliance;
            this.typeId = typeId;
            this.state = state;
            this.nextVulnerabilityDate = nextVulnerabilityDate;
            this.placementDate = placementDate;
            this.rewardTokenCount = rewardTokenCount;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            alliance.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            alliance = new AllianceInformations();
            alliance.Deserialize(reader);
        }


    }
}








