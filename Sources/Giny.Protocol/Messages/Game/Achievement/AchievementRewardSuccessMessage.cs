using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementRewardSuccessMessage : NetworkMessage  
    { 
        public  const ushort Id = 6364;
        public override ushort MessageId => Id;

        public short achievementId;

        public AchievementRewardSuccessMessage()
        {
        }
        public AchievementRewardSuccessMessage(short achievementId)
        {
            this.achievementId = achievementId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)achievementId);
        }
        public override void Deserialize(IDataReader reader)
        {
            achievementId = (short)reader.ReadShort();
        }


    }
}








