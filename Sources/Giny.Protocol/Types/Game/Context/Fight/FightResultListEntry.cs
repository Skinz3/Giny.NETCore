using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightResultListEntry  
    { 
        public const ushort Id = 7834;
        public virtual ushort TypeId => Id;

        public short outcome;
        public byte wave;
        public FightLoot rewards;

        public FightResultListEntry()
        {
        }
        public FightResultListEntry(short outcome,byte wave,FightLoot rewards)
        {
            this.outcome = outcome;
            this.wave = wave;
            this.rewards = rewards;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort((short)outcome);
            if (wave < 0)
            {
                throw new System.Exception("Forbidden value (" + wave + ") on element wave.");
            }

            writer.WriteByte((byte)wave);
            rewards.Serialize(writer);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            outcome = (short)reader.ReadVarUhShort();
            if (outcome < 0)
            {
                throw new System.Exception("Forbidden value (" + outcome + ") on element of FightResultListEntry.outcome.");
            }

            wave = (byte)reader.ReadByte();
            if (wave < 0)
            {
                throw new System.Exception("Forbidden value (" + wave + ") on element of FightResultListEntry.wave.");
            }

            rewards = new FightLoot();
            rewards.Deserialize(reader);
        }


    }
}








