using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.AI
{
    public class HealAction : AIAction
    {
        public HealAction(AIFighter fighter) : base(fighter)
        {
        }



        protected override void Apply()
        {
            foreach (var spellRecord in Fighter.GetSpells().Where(x => x.Category.HasFlag(SpellCategoryEnum.Healing)))
            {
                foreach (var ally in Fighter.Team.GetFighters<Fighter>().OrderBy(x => x.Stats.LifePercentage))
                {
                    if (ally.Stats.LifePercentage < 100)
                    {
                        Fighter.CastSpell(spellRecord.Id, ally.Cell.Id);
                    }
                }
            }

        }
    }
}