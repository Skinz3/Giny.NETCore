using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightResultMutantListEntry : FightResultFighterListEntry  
    { 
        public new const ushort Id = 2535;
        public override ushort TypeId => Id;

        public short level;

        public FightResultMutantListEntry()
        {
        }
        public FightResultMutantListEntry(short level,short outcome,byte wave,FightLoot rewards,double id,bool alive)
        {
            this.level = level;
            this.outcome = outcome;
            this.wave = wave;
            this.rewards = rewards;
            this.id = id;
            this.alive = alive;
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
                throw new System.Exception("Forbidden value (" + level + ") on element of FightResultMutantListEntry.level.");
            }

        }


    }
}








