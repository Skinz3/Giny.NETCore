using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class CharacterMinimalAllianceInformations : CharacterMinimalPlusLookInformations
    {
        public new const ushort Id = 1850;
        public override ushort TypeId => Id;

        public BasicNamedAllianceInformations alliance;

        public CharacterMinimalAllianceInformations()
        {
        }
        public CharacterMinimalAllianceInformations(BasicNamedAllianceInformations alliance, long id, string name, short level, EntityLook entityLook, byte breed)
        {
            this.alliance = alliance;
            this.id = id;
            this.name = name;
            this.level = level;
            this.entityLook = entityLook;
            this.breed = breed;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            alliance.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            alliance = new BasicNamedAllianceInformations();
            alliance.Deserialize(reader);
        }


    }
}


