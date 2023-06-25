using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PlayerSearchCharacterNameInformation : AbstractPlayerSearchInformation  
    { 
        public new const ushort Id = 8198;
        public override ushort TypeId => Id;

        public string name;

        public PlayerSearchCharacterNameInformation()
        {
        }
        public PlayerSearchCharacterNameInformation(string name)
        {
            this.name = name;
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








