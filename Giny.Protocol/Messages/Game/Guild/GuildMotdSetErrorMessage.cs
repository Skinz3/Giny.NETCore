using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildMotdSetErrorMessage : SocialNoticeSetErrorMessage  
    { 
        public new const ushort Id = 7761;
        public override ushort MessageId => Id;


        public GuildMotdSetErrorMessage()
        {
        }
        public GuildMotdSetErrorMessage(byte reason)
        {
            this.reason = reason;
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








