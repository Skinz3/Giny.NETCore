using Giny.Core.Extensions;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Zones.Sets;
using Giny.World.Managers.Maps;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.AI
{
    public abstract class AIAction
    {
        protected AIFighter Fighter
        {
            get;
            private set;
        }

        public AIAction(AIFighter fighter)
        {
            this.Fighter = fighter;
        }

        /*
         * Execute action
         */
        public void Execute()
        {
            if (Fighter.Alive)
            {
                Apply();
            }
        }
        protected abstract void Apply();

        protected IEnumerable<SpellRecord> GetSpells()
        {
            return Fighter.GetSpells();
        }

        protected MapPoint GetTargetPoint(short spellId, Func<MapPoint, bool> predicate)
        {
            var spell = Fighter.GetSpell(spellId);
            Set zone = Fighter.GetSpellZone(spell.Level, Fighter.Cell.Point);
            IEnumerable<MapPoint> points = zone.EnumerateValidPoints();
            MapPoint targetPoint = points.Shuffle().FirstOrDefault(predicate);
            return targetPoint;
        }

        protected IEnumerable<CellRecord> EnumeratePossiblePosition()
        {
            if (Fighter.IsTackled())
            {
                return new List<CellRecord>();
            }

            return Fighter.Fight.Map.Cells.Where(x => x.Point.ManhattanDistanceTo(Fighter.Cell.Point) <= Fighter.Stats.MovementPoints.TotalInContext()
                && x.Walkable && Fighter.Fight.IsCellFree(x));
        }
    }
}