using Giny.Core.Extensions;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.AI
{
    public class MoveToTarget : AIAction
    {
        public MoveToTarget(AIFighter fighter) : base(fighter)
        {

        }

        protected override void Apply()
        {
            var target = Fighter.EnemyTeam.CloserFighter(Fighter);

            if (target == null || target.IsMeleeWith(Fighter))
            {
                return;
            }

            foreach (var spellRecord in GetSpells().Where(x => x.Category == SpellCategoryEnum.Teleport).Shuffle())
            {
                var spell = Fighter.GetSpell(spellRecord.Id);
                var points = Fighter.GetSpellZone(spell.Level, Fighter.Cell.Point).EnumerateValidPoints();

                var targetPoint = points.Where(x => Fighter.Fight.IsCellFree(x.CellId)).MinBy(x => x.ManhattanDistanceTo(target.Cell.Point));

                if (targetPoint != null)
                {
                    Fighter.CastSpell(spellRecord.Id, targetPoint.CellId);
                }
            }

            var path = Fighter.FindPath(target);
            Fighter.Move(path);
        }
    }
}