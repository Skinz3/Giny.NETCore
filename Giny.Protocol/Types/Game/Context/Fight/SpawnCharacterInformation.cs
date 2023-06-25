using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SpawnCharacterInformation : SpawnInformation  
    { 
        public new const ushort Id = 6870;
        public override ushort TypeId => Id;

        public string name;
        public short level;

        public SpawnCharacterInformation()
        {
        }
        public SpawnCharacterInformation(string name,short level)
        {
            this.name = name;
            this.level = level;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)name);
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = (string)reader.ReadUTF();
            level = (short)reader.ReadVarUhShort();
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of SpawnCharacterInformation.level.");
            }

        }


    }
}








