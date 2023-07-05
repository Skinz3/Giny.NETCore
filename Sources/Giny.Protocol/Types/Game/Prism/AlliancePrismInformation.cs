using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AlliancePrismInformation : PrismInformation
    {
        public new const ushort Id = 5492;
        public override ushort TypeId => Id;

        public AllianceInformation alliance;

        public AlliancePrismInformation()
        {
        }
        public AlliancePrismInformation(AllianceInformation alliance, byte state, int placementDate, int nuggetsCount, int durability, double nextEvolutionDate)
        {
            this.alliance = alliance;
            this.state = state;
            this.placementDate = placementDate;
            this.nuggetsCount = nuggetsCount;
            this.durability = durability;
            this.nextEvolutionDate = nextEvolutionDate;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            alliance.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            alliance = new AllianceInformation();
            alliance.Deserialize(reader);
        }


    }
}


