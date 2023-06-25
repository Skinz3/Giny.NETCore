using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AchievementObjective  
    { 
        public const ushort Id = 6663;
        public virtual ushort TypeId => Id;

        public int id;
        public short maxValue;

        public AchievementObjective()
        {
        }
        public AchievementObjective(int id,short maxValue)
        {
            this.id = id;
            this.maxValue = maxValue;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarInt((int)id);
            if (maxValue < 0)
            {
                throw new System.Exception("Forbidden value (" + maxValue + ") on element maxValue.");
            }

            writer.WriteVarShort((short)maxValue);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (int)reader.ReadVarUhInt();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of AchievementObjective.id.");
            }

            maxValue = (short)reader.ReadVarUhShort();
            if (maxValue < 0)
            {
                throw new System.Exception("Forbidden value (" + maxValue + ") on element of AchievementObjective.maxValue.");
            }

        }


    }
}








