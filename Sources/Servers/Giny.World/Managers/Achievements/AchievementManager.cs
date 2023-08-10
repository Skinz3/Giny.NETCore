using Giny.Core.DesignPattern;
using Giny.Protocol.Types;
using Giny.World.Managers.Criterions.Handlers;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Achievements;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Achievements
{
    public class AchievementManager : Singleton<AchievementManager>
    {
        private readonly Dictionary<Type, List<AchievementObjectiveRecord>> AchievementObjectives = new Dictionary<Type, List<AchievementObjectiveRecord>>();

        private readonly object m_locker = new object();

        [StartupInvoke("Achievements", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            foreach (var achievementObjective in AchievementObjectiveRecord.GetAchievementObjectives())
            {
                foreach (var criteriaHandler in achievementObjective.CriteriaExpression.FindCriterionHandlers())
                {
                    AddAchievementObjective(criteriaHandler, achievementObjective);
                }
            }
        }


        public void OnPlayerChangeLevel(Character character)
        {
            TriggerAchievementsByCriterion<LevelCriterion>(character);
        }
        public void OnPlayerReachObjective(Character character)
        {
            TriggerAchievementsByCriterion<AchievementPointsCriterion>(character);
        }

        public void OnPlayerFightEnding(CharacterFighter characterFighter)
        {
            TriggerAchievementsByCriterion<KillMonsterWithChallengeCriterion>(characterFighter.Character);

          
        }


        public void TriggerAchievementsByCriterion<T>(Character character) where T : Criterion
        {
            lock (m_locker)
            {
                var objectives = AchievementObjectives[typeof(T)];

                foreach (var objective in objectives)
                {
                    if (objective.CriteriaExpression.Eval(character.Client))
                    {
                        var record = AchievementRecord.GetAchievement(objective.AchievementId);

                        if (record != null)
                        {
                            character.ReachAchievementObjective(record, objective);
                        }
                    }
                }
            }

        }
        public void OnPlayerChangeSubarea(Character character)
        {
            if (character.Map.Subarea.AchievementRecord != null)
            {
                character.ReachAchievement(character.Map.Subarea.AchievementRecord);
            }
        }
        private void AddAchievementObjective(Criterion criteria, AchievementObjectiveRecord obj)
        {
            var type = criteria.GetType();

            if (!AchievementObjectives.ContainsKey(type))
            {
                AchievementObjectives.Add(type, new List<AchievementObjectiveRecord>() { obj });
            }
            else
            {
                AchievementObjectives[type].Add(obj);
            }
        }


    }
}
