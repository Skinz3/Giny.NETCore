using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AchievementAchievedRewardable : AchievementAchieved  
    { 
        public new const ushort Id = 7808;
        public override ushort TypeId => Id;

        public short finishedlevel;

        public AchievementAchievedRewardable()
        {
        }
        public AchievementAchievedRewardable(short finishedlevel,short id,long achievedBy)
        {
            this.finishedlevel = finishedlevel;
            this.id = id;
            this.achievedBy = achievedBy;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (finishedlevel < 0 || finishedlevel > 200)
            {
                throw new System.Exception("Forbidden value (" + finishedlevel + ") on element finishedlevel.");
            }

            writer.WriteVarShort((short)finishedlevel);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            finishedlevel = (short)reader.ReadVarUhShort();
            if (finishedlevel < 0 || finishedlevel > 200)
            {
                throw new System.Exception("Forbidden value (" + finishedlevel + ") on element of AchievementAchievedRewardable.finishedlevel.");
            }

        }


    }
}








