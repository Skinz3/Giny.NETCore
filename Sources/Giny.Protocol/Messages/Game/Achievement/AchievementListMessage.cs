using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementListMessage : NetworkMessage  
    { 
        public  const ushort Id = 9613;
        public override ushort MessageId => Id;

        public AchievementAchieved[] finishedAchievements;

        public AchievementListMessage()
        {
        }
        public AchievementListMessage(AchievementAchieved[] finishedAchievements)
        {
            this.finishedAchievements = finishedAchievements;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)finishedAchievements.Length);
            for (uint _i1 = 0;_i1 < finishedAchievements.Length;_i1++)
            {
                writer.WriteShort((short)(finishedAchievements[_i1] as AchievementAchieved).TypeId);
                (finishedAchievements[_i1] as AchievementAchieved).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            AchievementAchieved _item1 = null;
            uint _finishedAchievementsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _finishedAchievementsLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<AchievementAchieved>((short)_id1);
                _item1.Deserialize(reader);
                finishedAchievements[_i1] = _item1;
            }

        }


    }
}








