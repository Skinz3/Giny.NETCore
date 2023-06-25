using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class TaxCollectorStaticExtendedInformations : TaxCollectorStaticInformations  
    { 
        public new const ushort Id = 7412;
        public override ushort TypeId => Id;

        public AllianceInformations allianceIdentity;

        public TaxCollectorStaticExtendedInformations()
        {
        }
        public TaxCollectorStaticExtendedInformations(AllianceInformations allianceIdentity,short firstNameId,short lastNameId,GuildInformations guildIdentity,long callerId)
        {
            this.allianceIdentity = allianceIdentity;
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.guildIdentity = guildIdentity;
            this.callerId = callerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceIdentity.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceIdentity = new AllianceInformations();
            allianceIdentity.Deserialize(reader);
        }


    }
}








