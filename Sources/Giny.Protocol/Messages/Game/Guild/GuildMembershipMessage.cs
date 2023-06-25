using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildMembershipMessage : GuildJoinedMessage  
    { 
        public new const ushort Id = 2565;
        public override ushort MessageId => Id;


        public GuildMembershipMessage()
        {
        }
        public GuildMembershipMessage(GuildInformations guildInfo,int rankId)
        {
            this.guildInfo = guildInfo;
            this.rankId = rankId;
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








