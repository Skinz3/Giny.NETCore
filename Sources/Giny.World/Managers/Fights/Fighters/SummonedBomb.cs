using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Fights.Movements;
using Giny.World.Records.Maps;
using Giny.World.Records.Monsters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Fighters
{
    public class SummonedBomb : SummonedMonster
    {
        private List<Wall> Walls
        {
            get;
            set;
        }
        public SummonedBomb(Fighter owner, MonsterRecord record, SpellEffectHandler summoningEffect, byte gradeId, CellRecord cell) : base(owner, record, summoningEffect, gradeId, cell)
        {
            this.Walls = new List<Wall>();
        }
        public override void OnSummoned()
        {
            base.OnSummoned();
            UpdateWalls();
        }

        public override void OnMove(Movement movement)
        {
            base.OnMove(movement);
            UpdateWalls();
        }

        private void UpdateWalls()
        {
             
        }
        public override void OnDie(Fighter killedBy)
        {
            base.OnDie(killedBy);
            UpdateWalls();
        }
        public override bool CanPlay()
        {
            return false;
        }
        public override bool DisplayInTimeline()
        {
            return false;
        }
    }
}
