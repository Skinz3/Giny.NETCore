using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AllianceInformations : BasicNamedAllianceInformations  
    { 
        public new const ushort Id = 6476;
        public override ushort TypeId => Id;

        public GuildEmblem allianceEmblem;

        public AllianceInformations()
        {
        }
        public AllianceInformations(GuildEmblem allianceEmblem,int allianceId,string allianceTag,string allianceName)
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
            allianceEmblem = new GuildEmblem();
            allianceEmblem.Deserialize(reader);
        }


    }
}








