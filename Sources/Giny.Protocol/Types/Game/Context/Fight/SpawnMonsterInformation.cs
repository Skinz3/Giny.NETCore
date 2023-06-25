using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SpawnMonsterInformation : BaseSpawnMonsterInformation  
    { 
        public new const ushort Id = 6061;
        public override ushort TypeId => Id;

        public byte creatureGrade;

        public SpawnMonsterInformation()
        {
        }
        public SpawnMonsterInformation(byte creatureGrade,short creatureGenericId)
        {
            this.creatureGrade = creatureGrade;
            this.creatureGenericId = creatureGenericId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (creatureGrade < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureGrade + ") on element creatureGrade.");
            }

            writer.WriteByte((byte)creatureGrade);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            creatureGrade = (byte)reader.ReadByte();
            if (creatureGrade < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureGrade + ") on element of SpawnMonsterInformation.creatureGrade.");
            }

        }


    }
}








