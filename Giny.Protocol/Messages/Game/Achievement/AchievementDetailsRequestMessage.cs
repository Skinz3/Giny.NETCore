using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementDetailsRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 4293;
        public override ushort MessageId => Id;

        public short achievementId;

        public AchievementDetailsRequestMessage()
        {
        }
        public AchievementDetailsRequestMessage(short achievementId)
        {
            this.achievementId = achievementId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (achievementId < 0)
            {
                throw new System.Exception("Forbidden value (" + achievementId + ") on element achievementId.");
            }

            writer.WriteVarShort((short)achievementId);
        }
        public override void Deserialize(IDataReader reader)
        {
            achievementId = (short)reader.ReadVarUhShort();
            if (achievementId < 0)
            {
                throw new System.Exception("Forbidden value (" + achievementId + ") on element of AchievementDetailsRequestMessage.achievementId.");
            }

        }


    }
}








