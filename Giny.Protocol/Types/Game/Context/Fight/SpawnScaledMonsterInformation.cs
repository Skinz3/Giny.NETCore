using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class SpawnScaledMonsterInformation : BaseSpawnMonsterInformation  
    { 
        public new const ushort Id = 5556;
        public override ushort TypeId => Id;

        public short creatureLevel;

        public SpawnScaledMonsterInformation()
        {
        }
        public SpawnScaledMonsterInformation(short creatureLevel,short creatureGenericId)
        {
            this.creatureLevel = creatureLevel;
            this.creatureGenericId = creatureGenericId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (creatureLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureLevel + ") on element creatureLevel.");
            }

            writer.WriteShort((short)creatureLevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            creatureLevel = (short)reader.ReadShort();
            if (creatureLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureLevel + ") on element of SpawnScaledMonsterInformation.creatureLevel.");
            }

        }


    }
}








