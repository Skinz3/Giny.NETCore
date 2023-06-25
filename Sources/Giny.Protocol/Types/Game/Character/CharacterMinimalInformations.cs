using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterMinimalInformations : CharacterBasicMinimalInformations  
    { 
        public new const ushort Id = 4872;
        public override ushort TypeId => Id;

        public short level;

        public CharacterMinimalInformations()
        {
        }
        public CharacterMinimalInformations(short level,long id,string name)
        {
            this.level = level;
            this.id = id;
            this.name = name;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of CharacterMinimalInformations.level.");
            }

        }


    }
}








