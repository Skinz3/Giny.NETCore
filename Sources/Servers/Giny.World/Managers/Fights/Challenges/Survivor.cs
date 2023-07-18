using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    [Challenge(33)]
    internal class Survivor : Challenge
    {
        public Survivor(ChallengeRecord record, FightTeam team) : base(record, team)
        {
        }

        public override double XpBonusRatio => 0.5;

        public override double DropBonusRatio => 0.5;

        public override void BindEvents()
        {

        }

        public override IEnumerable<Fighter> GetConcernedFighters()
        {
            return Team.GetFighters<Fighter>();
        }

        public override void UnbindEvents()
        {

        }
    }
}
