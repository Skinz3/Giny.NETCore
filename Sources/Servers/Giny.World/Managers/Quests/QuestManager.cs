using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Entities.Npcs;
using Giny.World.Managers.Formulas;
using Giny.World.Records.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Quests
{
    public class QuestManager : Singleton<QuestManager>
    {
        public CharacterQuestRecord CreateCharacterQuest(QuestRecord record)
        {
            return new CharacterQuestRecord()
            {
                Objectives = record.Steps.First().Objectives.Select(x => x.GetCharacterQuestObjectiveRecord()).ToList(),
                QuestId = record.Id,
                StepId = record.StepIds.First(),
            };
        }

        public void OnNpcTalk(Character character, Npc npc)
        {
            var quests = character.GetActiveQuests();

            foreach (var quest in quests)
            {
                var objective = quest.GetObjectives(QuestObjectiveTypeEnum.GoToNpc, QuestObjectiveTypeEnum.NpcTalkBack).FirstOrDefault(x => !x.Done && x.Record.Parameters.Param0 == npc.Template.Id);

                if (objective != null && quest.Available(objective))
                {
                    character.CompleteQuestObjective(quest, objective);
                }
            }
        }

        public void ApplyRewards(Character character, CharacterQuestRecord quest)
        {
            foreach (var reward in quest.StepRecord.Rewards)
            {

                foreach (var item in reward.ItemRewards)
                {
                    var characterItem = character.Inventory.AddItem(item.ItemId, item.Quantity);

                    if (characterItem != null)
                    {
                        character.NotifyItemGained(item.ItemId,item.Quantity);
                    }
                }

                foreach (short titleId in reward.TitlesReward)
                {
                    character.LearnTitle(titleId);
                }

                foreach (short emoteId in reward.EmoteReward)
                {
                    character.LearnEmote(emoteId);
                }

                if (reward.KamasRatio > 0)
                {
                    long value = AchievementsFormulas.Instance.GetKamasReward(reward.KamasScaleWithPlayerLevel,
                        reward.LevelMin, reward.KamasRatio, 1, character.SafeLevel);

                    character.AddKamas(value);
                }

                foreach (short spellId in reward.SpellsReward)
                {
                    character.LearnSpell(spellId, true);
                }

                if (reward.ExperienceRatio > 0)
                {
                    long value = AchievementsFormulas.Instance.GetExperienceReward(character.SafeLevel,
                         0, reward.LevelMin, reward.ExperienceRatio, 1);

                    //character.AddExperience(value);
                }

              
            }
        }
    }
}
