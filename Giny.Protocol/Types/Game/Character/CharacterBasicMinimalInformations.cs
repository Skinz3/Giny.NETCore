using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterBasicMinimalInformations : AbstractCharacterInformation  
    { 
        public new const ushort Id = 576;
        public override ushort TypeId => Id;

        public string name;

        public CharacterBasicMinimalInformations()
        {
        }
        public CharacterBasicMinimalInformations(string name,long id)
        {
            this.name = name;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)name);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = (string)reader.ReadUTF();
        }


    }
}








