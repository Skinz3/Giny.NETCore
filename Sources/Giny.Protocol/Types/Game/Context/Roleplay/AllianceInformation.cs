using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AllianceInformation : BasicNamedAllianceInformations
    {
        public new const ushort Id = 7208;
        public override ushort TypeId => Id;

        public SocialEmblem allianceEmblem;

        public AllianceInformation()
        {
        }
        public AllianceInformation(SocialEmblem allianceEmblem, int allianceId, string allianceTag, string allianceName)
        {
            this.allianceEmblem = allianceEmblem;
            this.allianceId = allianceId;
            this.allianceTag = allianceTag;
            this.allianceName = allianceName;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceEmblem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceEmblem = new SocialEmblem();
            allianceEmblem.Deserialize(reader);
        }


    }
}


