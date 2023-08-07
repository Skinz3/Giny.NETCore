using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Handlers.Roleplay.Achievements
{
    public class AchievementsHandler
    {
        [MessageHandler]
        public static void HandleAchievementRewardRequest(AchievementRewardRequestMessage message, WorldClient client)
        {
            if (message.achievementId == -1)
            {
                client.Character.RewardAllAchievements();
            }
            else
            {
                client.Character.RewardAchievement(message.achievementId);
            }
        }

        [MessageHandler]
        public static void HandleAchievementAlmostFinishedDetailedListRequest(AchievementAlmostFinishedDetailedListRequestMessage message, WorldClient client)
        {
          //  client.Send(new AchievementAlmostFinishedDetailedListMessage(new Protocol.Types.Achievement()))
        }
    }
}
