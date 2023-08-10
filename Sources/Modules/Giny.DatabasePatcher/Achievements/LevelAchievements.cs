using Giny.Core;
using Giny.ORM;
using Giny.World.Records.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.DatabasePatcher.Achievements
{
    public class LevelAchievements
    {
        public static void Patch()
        {
            Logger.Write("Patching level achivements...", Channels.Info);

            AddAchievementObjective(3, 185, "PL>9");
            AddAchievementObjective(4, 190, "PL>19");
            AddAchievementObjective(5, 187, "PL>39");
            AddAchievementObjective(6, 191, "PL>59");
            AddAchievementObjective(7, 183, "PL>79");
            AddAchievementObjective(8, 189, "PL>99");
            AddAchievementObjective(9, 184, "PL>119");
            AddAchievementObjective(10, 188, "PL>139");
            AddAchievementObjective(11, 186, "PL>159");
            AddAchievementObjective(12, 192, "PL>179");
            AddAchievementObjective(13, 193, "PL>199");

            
        }

        private static void AddAchievementObjective(int achievementId, int achievementObjectiveId, string criterion)
        {
            var achievementRecord = AchievementRecord.GetAchievement(achievementId);

            if (achievementRecord == null)
            {
                Logger.Write("Unable to find achievement (" + achievementRecord.Name + ")", Channels.Warning);
                return;
            }

            var exists = AchievementObjectiveRecord.GetAchievementObjectives().Any(x => x.Id == achievementObjectiveId);

            if (exists)
            {
                Logger.Write("Duplicate achievement objective " + achievementObjectiveId + " aborting.", Channels.Warning);
                return;
            }

            if (!achievementRecord.ObjectiveIds.Contains(achievementObjectiveId))
            {
                achievementRecord.ObjectiveIds.Add(achievementObjectiveId);
            }


            AchievementObjectiveRecord objective = new AchievementObjectiveRecord()
            {
                AchievementId = achievementId,
                Criterion = criterion,
                Id = achievementObjectiveId,
                Name = achievementRecord.Name,
                ObjectiveOrder = 0,
            };

            achievementRecord.ReloadMembers();

            objective.AddInstantElement();
            achievementRecord.UpdateInstantElement();
        }
    }
}
