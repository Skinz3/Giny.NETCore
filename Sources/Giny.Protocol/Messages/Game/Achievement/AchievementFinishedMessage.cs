using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementFinishedMessage : NetworkMessage  
    { 
        public  const ushort Id = 6489;
        public override ushort MessageId => Id;

        public AchievementAchievedRewardable achievement;

        public AchievementFinishedMessage()
        {
        }
        public AchievementFinishedMessage(AchievementAchievedRewardable achievement)
        {
            this.achievement = achievement;
        }
        public override void Serialize(IDataWriter writer)
        {
            achievement.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            achievement = new AchievementAchievedRewardable();
            achievement.Deserialize(reader);
        }


    }
}








