using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementAlmostFinishedDetailedListMessage : NetworkMessage  
    { 
        public  const ushort Id = 133;
        public override ushort MessageId => Id;

        public Achievement[] almostFinishedAchievements;

        public AchievementAlmostFinishedDetailedListMessage()
        {
        }
        public AchievementAlmostFinishedDetailedListMessage(Achievement[] almostFinishedAchievements)
        {
            this.almostFinishedAchievements = almostFinishedAchievements;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)almostFinishedAchievements.Length);
            for (uint _i1 = 0;_i1 < almostFinishedAchievements.Length;_i1++)
            {
                (almostFinishedAchievements[_i1] as Achievement).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            Achievement _item1 = null;
            uint _almostFinishedAchievementsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _almostFinishedAchievementsLen;_i1++)
            {
                _item1 = new Achievement();
                _item1.Deserialize(reader);
                almostFinishedAchievements[_i1] = _item1;
            }

        }


    }
}








