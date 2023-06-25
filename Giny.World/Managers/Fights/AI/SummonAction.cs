using Giny.Core.Extensions;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.AI
{
    public class SummonAction : AIAction
    {
        public SummonAction(AIFighter fighter) : base(fighter)
        {

        }


        protected override void Apply()
        {
            if (!Fighter.CanSummon())
            {
                return;
            }

            foreach (var spellRecord in GetSpells().Where(x => x.Category == SpellCategoryEnum.Summon).Shuffle())
            {
                MapPoint targetPoint = GetTargetPoint(spellRecord.Id, x => Fighter.Fight.IsCellFree(x.CellId));

                if (targetPoint != null)
                {
                    Fighter.CastSpell(spellRecord.Id, targetPoint.CellId);
                }
            }
        }
    }
}