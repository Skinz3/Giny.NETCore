using Giny.Core.Extensions;
using Giny.World.Managers.Effects.Targets;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.AI
{
    public class BuffAction : AIAction
    {
        public BuffAction(AIFighter fighter) : base(fighter)
        {

        }


        protected override void Apply()
        {
            var monsters = Fighter.Team.GetFighters<MonsterFighter>();
            var summons = Fighter.Team.GetFighters<SummonedMonster>();

            foreach (var spellRecord in Fighter.GetSpells().Where(x => x.Category.HasFlag(SpellCategoryEnum.Buff) && !x.Category.HasFlag(SpellCategoryEnum.Debuff) && !x.Category.HasFlag(SpellCategoryEnum.Agressive)))
            {
                var spell = Fighter.GetSpell(spellRecord.Id);

                var targets = GetTargetMonsters(spell.Level);

                foreach (var target in targets)
                {
                    var targetMonster = monsters.FirstOrDefault(x => x.Record.Id == target);

                    if (targetMonster != null)
                    {
                        Fighter.CastSpell(spellRecord.Id, targetMonster.Cell.Id);
                    }

                    var targetSummon = summons.FirstOrDefault(x => x.Record.Id == target);

                    if (targetSummon != null)
                    {
                        Fighter.CastSpell(spellRecord.Id, targetSummon.Cell.Id);
                    }


                }

                // target in priority target mask 
                foreach (var ally in Fighter.Team.GetFighters<Fighter>().Shuffle().ToArray())
                {
                    Fighter.CastSpell(spellRecord.Id, ally.Cell.Id);
                }
            }
        }

        private List<int> GetTargetMonsters(SpellLevelRecord level)
        {
            List<int> ids = new List<int>();

            foreach (var effect in level.Effects)
            {
                foreach (var target in effect.GetTargets())
                {
                    if (target is MonsterCriterion targetMonster)
                    {
                        if (targetMonster.Required)
                        {
                            ids.Add(targetMonster.MonsterId);
                        }
                    }
                }
            }
            return ids;
        }
    }
}