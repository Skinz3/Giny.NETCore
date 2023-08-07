using Giny.Protocol.Types;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Achievements
{
    [ProtoContract]
    public class CharacterAchievement
    {
        [ProtoMember(1)]
        public short AchievementId
        {
            get;
            set;
        }

        [ProtoMember(2)]
        public bool Rewarded
        {
            get;
            set;
        }

        [ProtoMember(3)]
        public short FinishedLevel
        {
            get;
            set;
        }
        public CharacterAchievement()
        {

        }
        public CharacterAchievement(short achievementId, short finishedLevel, bool rewarded = false)
        {
            AchievementId = achievementId;
            FinishedLevel = finishedLevel;  
            Rewarded = rewarded;
        }

        public AchievementAchieved GetAchievementAchieved(long characterId)
        {
            return Rewarded ? new AchievementAchieved(AchievementId, characterId) : new AchievementAchievedRewardable(FinishedLevel, AchievementId, characterId);
        }
    }
}
