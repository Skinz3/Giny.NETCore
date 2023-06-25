using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementDetailsMessage : NetworkMessage  
    { 
        public  const ushort Id = 7443;
        public override ushort MessageId => Id;

        public Achievement achievement;

        public AchievementDetailsMessage()
        {
        }
        public AchievementDetailsMessage(Achievement achievement)
        {
            this.achievement = achievement;
        }
        public override void Serialize(IDataWriter writer)
        {
            achievement.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            achievement = new Achievement();
            achievement.Deserialize(reader);
        }


    }
}








