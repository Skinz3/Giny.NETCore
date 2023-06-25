using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterMinimalGuildInformations : CharacterMinimalPlusLookInformations  
    { 
        public new const ushort Id = 3487;
        public override ushort TypeId => Id;

        public BasicGuildInformations guild;

        public CharacterMinimalGuildInformations()
        {
        }
        public CharacterMinimalGuildInformations(BasicGuildInformations guild,long id,string name,short level,EntityLook entityLook,byte breed)
        {
            this.guild = guild;
            this.id = id;
            this.name = name;
            this.level = level;
            this.entityLook = entityLook;
            this.breed = breed;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            guild.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            guild = new BasicGuildInformations();
            guild.Deserialize(reader);
        }


    }
}








