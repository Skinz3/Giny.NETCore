using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterMinimalGuildPublicInformations : CharacterMinimalInformations  
    { 
        public new const ushort Id = 7655;
        public override ushort TypeId => Id;

        public GuildRankPublicInformation rank;

        public CharacterMinimalGuildPublicInformations()
        {
        }
        public CharacterMinimalGuildPublicInformations(GuildRankPublicInformation rank,long id,string name,short level)
        {
            this.rank = rank;
            this.id = id;
            this.name = name;
            this.level = level;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            rank.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            rank = new GuildRankPublicInformation();
            rank.Deserialize(reader);
        }


    }
}








