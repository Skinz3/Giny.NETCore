using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterMinimalAllianceInformations : CharacterMinimalGuildInformations  
    { 
        public new const ushort Id = 4632;
        public override ushort TypeId => Id;

        public BasicAllianceInformations alliance;

        public CharacterMinimalAllianceInformations()
        {
        }
        public CharacterMinimalAllianceInformations(BasicAllianceInformations alliance,long id,string name,short level,EntityLook entityLook,byte breed,BasicGuildInformations guild)
        {
            this.alliance = alliance;
            this.id = id;
            this.name = name;
            this.level = level;
            this.entityLook = entityLook;
            this.breed = breed;
            this.guild = guild;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            alliance.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            alliance = new BasicAllianceInformations();
            alliance.Deserialize(reader);
        }


    }
}








