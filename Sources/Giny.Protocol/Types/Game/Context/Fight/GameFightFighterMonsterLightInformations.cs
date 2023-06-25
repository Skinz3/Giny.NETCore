using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightFighterMonsterLightInformations : GameFightFighterLightInformations  
    { 
        public new const ushort Id = 87;
        public override ushort TypeId => Id;

        public short creatureGenericId;

        public GameFightFighterMonsterLightInformations()
        {
        }
        public GameFightFighterMonsterLightInformations(short creatureGenericId,double id,byte wave,short level,byte breed,bool sex,bool alive)
        {
            this.creatureGenericId = creatureGenericId;
            this.id = id;
            this.wave = wave;
            this.level = level;
            this.breed = breed;
            this.sex = sex;
            this.alive = alive;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (creatureGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureGenericId + ") on element creatureGenericId.");
            }

            writer.WriteVarShort((short)creatureGenericId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            creatureGenericId = (short)reader.ReadVarUhShort();
            if (creatureGenericId < 0)
            {
                throw new System.Exception("Forbidden value (" + creatureGenericId + ") on element of GameFightFighterMonsterLightInformations.creatureGenericId.");
            }

        }


    }
}








