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
     *  tous les PM disponibles au début du tour doivent être utilisés, c'est-à-dire que le challenge
     *  échoue si des PM sont perdus lors d'un tacle. Rester collé à un adversaire n'invalide pas le défi.
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

        public override IEnumerable<Fighter> GetConcernedFighters()
        {
            return Team.GetFighters<Fighter>();
        }

        private void OnTackled(Fighter fighter)
        {
            OnChallengeResulted(ChallengeResultEnum.Failed);
        }
        private void OnTurnEnded(Fighter fighter)
        {
            if (ConcernedFighters.Contains(fighter))
            {
                if (fighter.Stats.MovementPoints.TotalInContext() > 0)
                {
                    OnChallengeResulted(ChallengeResultEnum.Failed);
                }
            }
        }
    }
}
