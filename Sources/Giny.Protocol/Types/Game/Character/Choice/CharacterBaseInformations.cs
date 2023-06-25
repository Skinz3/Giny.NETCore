using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterBaseInformations : CharacterMinimalPlusLookInformations  
    { 
        public new const ushort Id = 1839;
        public override ushort TypeId => Id;

        public bool sex;

        public CharacterBaseInformations()
        {
        }
        public CharacterBaseInformations(bool sex,long id,string name,short level,EntityLook entityLook,byte breed)
        {
            this.sex = sex;
            this.id = id;
            this.name = name;
            this.level = level;
            this.entityLook = entityLook;
            this.breed = breed;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)sex);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            sex = (bool)reader.ReadBoolean();
        }


    }
}








