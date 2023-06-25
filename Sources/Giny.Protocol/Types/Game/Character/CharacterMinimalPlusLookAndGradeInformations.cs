using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterMinimalPlusLookAndGradeInformations : CharacterMinimalPlusLookInformations  
    { 
        public new const ushort Id = 5154;
        public override ushort TypeId => Id;

        public int grade;

        public CharacterMinimalPlusLookAndGradeInformations()
        {
        }
        public CharacterMinimalPlusLookAndGradeInformations(int grade,long id,string name,short level,EntityLook entityLook,byte breed)
        {
            this.grade = grade;
            this.id = id;
            this.name = name;
            this.level = level;
            this.entityLook = entityLook;
            this.breed = breed;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (grade < 0)
            {
                throw new System.Exception("Forbidden value (" + grade + ") on element grade.");
            }

            writer.WriteVarInt((int)grade);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            grade = (int)reader.ReadVarUhInt();
            if (grade < 0)
            {
                throw new System.Exception("Forbidden value (" + grade + ") on element of CharacterMinimalPlusLookAndGradeInformations.grade.");
            }

        }


    }
}








