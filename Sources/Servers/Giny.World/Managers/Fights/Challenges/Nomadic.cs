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
    /*
      *  Les combattants alliés doivent utiliser tous leurs points de mouvement à chaque tour 
      *  pendant toute la durée du combat. 
      */
    [Challenge(8)]
    public class Nomadic : Challenge
    {
        public Nomadic(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.30d;

        public override double DropBonusRatio => 0.20d;

        public override void BindEvents()
        {
            Fight.TurnEnded += OnTurnEnded;

            foreach (var fighter in ConcernedFighters)
            {
                fighter.Tackled += OnTackled;
            }
        }


        public override void UnbindEvents()
        {
            Fight.TurnEnded -= OnTurnEnded;

            foreach (var fighter in ConcernedFighters)
            {
                fighter.Tackled -= OnTackled;
            }
        }

        public override IEnumerable<Fighter> GetAffectedFighters()
        {
            return Team.GetFighters<Fighter>();
        }

        private void OnTackled(Fighter fighter)
        {
            OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
        }
        private void OnTurnEnded(Fighter fighter)
        {
            if (ConcernedFighters.Contains(fighter))
            {
                if (fighter.Stats.MovementPoints.TotalInContext() > 0)
                {
                    OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
                }
            }
        }
    }
}
