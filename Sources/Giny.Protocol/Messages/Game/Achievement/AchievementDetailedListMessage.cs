using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementDetailedListMessage : NetworkMessage  
    { 
        public  const ushort Id = 8447;
        public override ushort MessageId => Id;

        public Achievement[] startedAchievements;
        public Achievement[] finishedAchievements;

        public AchievementDetailedListMessage()
        {
        }
        public AchievementDetailedListMessage(Achievement[] startedAchievements,Achievement[] finishedAchievements)
        {
            this.startedAchievements = startedAchievements;
            this.finishedAchievements = finishedAchievements;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)startedAchievements.Length);
            for (uint _i1 = 0;_i1 < startedAchievements.Length;_i1++)
            {
                (startedAchievements[_i1] as Achievement).Serialize(writer);
            }

            writer.WriteShort((short)finishedAchievements.Length);
            for (uint _i2 = 0;_i2 < finishedAchievements.Length;_i2++)
            {
                (finishedAchievements[_i2] as Achievement).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            Achievement _item1 = null;
            Achievement _item2 = null;
            uint _startedAchievementsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _startedAchievementsLen;_i1++)
            {
                _item1 = new Achievement();
                _item1.Deserialize(reader);
                startedAchievements[_i1] = _item1;
            }

            uint _finishedAchievementsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _finishedAchievementsLen;_i2++)
            {
                _item2 = new Achievement();
                _item2.Deserialize(reader);
                finishedAchievements[_i2] = _item2;
            }

        }


    }
}








