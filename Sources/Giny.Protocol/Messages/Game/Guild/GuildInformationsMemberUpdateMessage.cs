using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildInformationsMemberUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 2768;
        public override ushort MessageId => Id;

        public GuildMember member;

        public GuildInformationsMemberUpdateMessage()
        {
        }
        public GuildInformationsMemberUpdateMessage(GuildMember member)
        {
            this.member = member;
        }
        public override void Serialize(IDataWriter writer)
        {
            member.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            member = new GuildMember();
            member.Deserialize(reader);
        }


    }
}








