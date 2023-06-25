using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceMembershipMessage : AllianceJoinedMessage  
    { 
        public new const ushort Id = 4163;
        public override ushort MessageId => Id;


        public AllianceMembershipMessage()
        {
        }
        public AllianceMembershipMessage(AllianceInformations allianceInfo,bool enabled,int leadingGuildId)
        {
            this.allianceInfo = allianceInfo;
            this.enabled = enabled;
            this.leadingGuildId = leadingGuildId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








