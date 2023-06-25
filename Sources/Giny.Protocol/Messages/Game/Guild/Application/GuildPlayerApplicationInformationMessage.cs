using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildPlayerApplicationInformationMessage : GuildPlayerApplicationAbstractMessage  
    { 
        public new const ushort Id = 1200;
        public override ushort MessageId => Id;

        public GuildInformations guildInformation;
        public GuildApplicationInformation apply;

        public GuildPlayerApplicationInformationMessage()
        {
        }
        public GuildPlayerApplicationInformationMessage(GuildInformations guildInformation,GuildApplicationInformation apply)
        {
            this.guildInformation = guildInformation;
            this.apply = apply;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guildInformation.Serialize(writer);
            apply.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildInformation = new GuildInformations();
            guildInformation.Deserialize(reader);
            apply = new GuildApplicationInformation();
            apply.Deserialize(reader);
        }


    }
}








