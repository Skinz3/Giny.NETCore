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
     * Pétulant demande à tous les joueurs d'utiliser la totalité de leurs PA (point d'action) à la fin de leur tour. 
     * Remarque : vous devez terminer le combat en ayant utilisé tous vos PA pour achever le dernier adversaire.
     */
    [Challenge(41)]
    public class Petulant : Challenge
    {
        public Petulant(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.30d;

        public override double DropBonusRatio => 0.20d;

        public override void BindEvents()
        {
            Fight.TurnEnded += OnTurnEnded;
        }

        public override IEnumerable<Fighter> GetConcernedFighters()
        {
            return Team.GetFighters<Fighter>();
        }

        public override void UnbindEvents()
        {
            Fight.TurnEnded -= OnTurnEnded;
        }

        private void OnTurnEnded(Fighter fighter)
        {
            if (ConcernedFighters.Contains(fighter))
            {
                if (fighter.Stats.ActionPoints.TotalInContext() > 0)
                {
                    OnChallengeResulted(ChallengeResultEnum.Failed);
                }
            }
        }
    }
}
