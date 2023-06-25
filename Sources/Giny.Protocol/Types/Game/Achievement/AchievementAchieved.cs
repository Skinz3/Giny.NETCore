using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AchievementAchieved  
    { 
        public const ushort Id = 7505;
        public virtual ushort TypeId => Id;

        public short id;
        public long achievedBy;

        public AchievementAchieved()
        {
        }
        public AchievementAchieved(short id,long achievedBy)
        {
            this.id = id;
            this.achievedBy = achievedBy;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
            if (achievedBy < 0 || achievedBy > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + achievedBy + ") on element achievedBy.");
            }

            writer.WriteVarLong((long)achievedBy);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of AchievementAchieved.id.");
            }

            achievedBy = (long)reader.ReadVarUhLong();
            if (achievedBy < 0 || achievedBy > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + achievedBy + ") on element of AchievementAchieved.achievedBy.");
            }

        }


    }
}








