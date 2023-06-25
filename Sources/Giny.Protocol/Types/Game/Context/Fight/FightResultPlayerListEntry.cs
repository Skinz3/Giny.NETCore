using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightResultPlayerListEntry : FightResultFighterListEntry  
    { 
        public new const ushort Id = 9475;
        public override ushort TypeId => Id;

        public short level;
        public FightResultAdditionalData[] additional;

        public FightResultPlayerListEntry()
        {
        }
        public FightResultPlayerListEntry(short level,FightResultAdditionalData[] additional,short outcome,byte wave,FightLoot rewards,double id,bool alive)
        {
            this.level = level;
            this.additional = additional;
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
            writer.WriteShort((short)additional.Length);
            for (uint _i2 = 0;_i2 < additional.Length;_i2++)
            {
                writer.WriteShort((short)(additional[_i2] as FightResultAdditionalData).TypeId);
                (additional[_i2] as FightResultAdditionalData).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            FightResultAdditionalData _item2 = null;
            base.Deserialize(reader);
            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of FightResultPlayerListEntry.level.");
            }

            uint _additionalLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _additionalLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<FightResultAdditionalData>((short)_id2);
                _item2.Deserialize(reader);
                additional[_i2] = _item2;
            }

        }


    }
}








