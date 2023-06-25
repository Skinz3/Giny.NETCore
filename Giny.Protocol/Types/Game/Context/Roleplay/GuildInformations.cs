using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildInformations : BasicGuildInformations  
    { 
        public new const ushort Id = 4148;
        public override ushort TypeId => Id;

        public GuildEmblem guildEmblem;

        public GuildInformations()
        {
        }
        public GuildInformations(GuildEmblem guildEmblem,int guildId,string guildName,byte guildLevel)
        {
            this.guildEmblem = guildEmblem;
            this.guildId = guildId;
            this.guildName = guildName;
            this.guildLevel = guildLevel;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guildEmblem.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildEmblem = new GuildEmblem();
            guildEmblem.Deserialize(reader);
        }


    }
}








