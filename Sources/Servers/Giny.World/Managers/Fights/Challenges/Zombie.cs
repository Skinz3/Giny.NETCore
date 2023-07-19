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
      *  Les combattants alliés doivent utiliser exactement un point de mouvement par tour de jeu.
          Perdre des points de mouvement en détaclant ne fait pas échouer le challenge.
      */
    [Challenge(1)]
    public class Zombie : Challenge
    {
        public Zombie(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.30d;

        public override double DropBonusRatio => 0.20d;

        public override void BindEvents()
        {
            Fight.TurnEnded += OnTurnEnded;

            foreach (var fighter in AffectedFighters)
            {
                fighter.Tackled += OnTackled;
            }
        }


        public override void UnbindEvents()
        {
            Fight.TurnEnded -= OnTurnEnded;

            foreach (var fighter in AffectedFighters)
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
            if (AffectedFighters.Contains(fighter))
            {
                if (fighter.Stats.MovementPoints.Used != 1)
                {
                    OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
                }
            }
        }
    }
}
