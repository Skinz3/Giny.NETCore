using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanOptionGuild : HumanOption  
    { 
        public new const ushort Id = 5744;
        public override ushort TypeId => Id;

        public GuildInformations guildInformations;

        public HumanOptionGuild()
        {
        }
        public HumanOptionGuild(GuildInformations guildInformations)
        {
            this.guildInformations = guildInformations;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guildInformations.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guildInformations = new GuildInformations();
            guildInformations.Deserialize(reader);
        }


    }
}








