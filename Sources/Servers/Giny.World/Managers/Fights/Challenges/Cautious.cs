using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    [Challenge(40)]
    public class Cautious : Challenge
    {
        public Cautious(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.50d;

        public override double DropBonusRatio => 0.20d;

        public override void BindEvents()
        {
            Fight.TurnEnded += OnTurnEnded;
        }

        public override void UnbindEvents()
        {
            Fight.TurnEnded -= OnTurnEnded;
        }

        private void OnTurnEnded(Fighter fighter)
        {
            if (ConcernedFighters.Contains(fighter))
            {
                if (fighter.GetMeleeFighters().Any(x => !x.IsFriendlyWith(fighter)))
                {
                    OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
                }
            }
        }


        public override IEnumerable<Fighter> GetAffectedFighters()
        {
            return Team.GetFighters<Fighter>();
        }
    }
}
