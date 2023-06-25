using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    [Challenge(50)]
    public class DevCheat : Challenge
    {
        public DevCheat(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.10d;

        public override double DropBonusRatio => 0.05d;

        public override void BindEvents()
        {

        }

        public override IEnumerable<Fighter> GetConcernedFighters()
        {
            return new Fighter[0];
        }

        public override void UnbindEvents()
        {

        }
    }
}
