using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    [Challenge(9)]
    public class Barbaric : Challenge
    {
        public Barbaric(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.10d;

        public override double DropBonusRatio => 0.10;

        public override void BindEvents()
        {
           
        }

        public override void UnbindEvents()
        {
           
        }

        public override IEnumerable<Fighter> GetAffectedFighters()
        {
            return Team.GetFighters<Fighter>();
        }
    }
}
