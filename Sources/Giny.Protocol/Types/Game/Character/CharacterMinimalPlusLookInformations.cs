using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations  
    { 
        public new const ushort Id = 6908;
        public override ushort TypeId => Id;

        public EntityLook entityLook;
        public byte breed;

        public CharacterMinimalPlusLookInformations()
        {
        }
        public CharacterMinimalPlusLookInformations(EntityLook entityLook,byte breed,long id,string name,short level)
        {
            this.entityLook = entityLook;
            this.breed = breed;
            this.id = id;
            this.name = name;
            this.level = level;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            entityLook.Serialize(writer);
            writer.WriteByte((byte)breed);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            entityLook = new EntityLook();
            entityLook.Deserialize(reader);
            breed = (byte)reader.ReadByte();
        }


    }
}








