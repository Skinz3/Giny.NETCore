using Giny.Core.DesignPattern;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Formulas
{
    public class AchievementsFormulas : Singleton<AchievementsFormulas>
    {
        private const double REWARD_SCALE_CAP = 1.5;

        private const double REWARD_REDUCED_SCALE = 0.7;

        public long GetKamasReward(bool kamasScaleWithPlayerLevel, int optimalLevel = -1, double kamasRatio = 1, double duration = 1, int pPlayerLevel = -1)
        {
            var lvl = kamasScaleWithPlayerLevel ? pPlayerLevel : optimalLevel;
            return (long)Math.Floor((Math.Pow(lvl, 2) + 20 * lvl - 20) * kamasRatio * duration);
        }

        public long GetExperienceReward(int pPlayerLevel, int pXpBonus, int optimalLevel, double xpRatio, int duration)
        {
            double xpBonus = 1 + pXpBonus / 100;

            if (pPlayerLevel > optimalLevel)
            {
                double rewardLevel = Math.Min(pPlayerLevel, optimalLevel * REWARD_SCALE_CAP);
                double fixeOptimalLevelExperienceReward = GetFixeExperienceReward(optimalLevel, duration, xpRatio);
                double fixeLevelExperienceReward = GetFixeExperienceReward((int)rewardLevel, duration, xpRatio);
                double reducedOptimalExperienceReward = (1 - REWARD_REDUCED_SCALE) * fixeOptimalLevelExperienceReward;
                double reducedExperienceReward = REWARD_REDUCED_SCALE * fixeLevelExperienceReward;
                double sumExperienceRewards = Math.Floor(reducedOptimalExperienceReward + reducedExperienceReward);
                return (long)Math.Floor(sumExperienceRewards * xpBonus);
            }
            return (long)Math.Floor(this.GetFixeExperienceReward(pPlayerLevel, duration, xpRatio) * xpBonus);
        }
        private double GetFixeExperienceReward(int level, double duration, double xpRatio)
        {
            var levelPow = Math.Pow(100 + 2 * level, 2);
            return level * levelPow / 20 * duration * xpRatio;
        }

    }
}
