using Giny.Core.Network.Messages;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Network;
using Giny.World.Records.Achievements;
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

        [MessageHandler]
        public static void HandleAchievementDetailsRequestMessage(AchievementDetailsRequestMessage message, WorldClient client)
        {
            var achievement = client.Character.GetAchievement(message.achievementId);

            if (achievement != null)
            {
                client.Send(new AchievementDetailsMessage(achievement.GetAchievement(client.Character)));
            }
            else
            {
                AchievementRecord record = AchievementRecord.GetAchievement(message.achievementId);

                if (record != null)
                {
                    client.Send(new AchievementDetailsMessage(record.GetAchievement(client.Character)));
                }
            }

            // client.Send(new AchievementDetailsMessage(result));
        }

        [MessageHandler]
        public static void HandleAchievementDetailedListRequestMessage(AchievementDetailedListRequestMessage message, WorldClient client)
        {
            client.Character.SendAchievementDetailedList(message.categoryId);
          
        }
    }
}
