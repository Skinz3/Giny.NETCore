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
     * Aucun personnage ne doit terminer son tour sur une cellule adjacente d'un allié.
     * À noter que le challenge n'échoue pas lorsqu'un personnage est porté à la fin d'un tour.
     */
    [Challenge(39)]
    public class Anchorite : Challenge
    {
        public Anchorite(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.50d;

        public override double DropBonusRatio => 0.30d;

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
                if (fighter.GetMeleeFighters().Any(x => x != fighter && x.IsFriendlyWith(fighter)))
                {
                    OnChallengeResulted(ChallengeResultEnum.Failed);
                }
            }
        }
        public override bool IsValid()
        {
            return Team.GetFightersCount() > 1;
        }

        public override IEnumerable<Fighter> GetConcernedFighters()
        {
            return Team.GetFighters<Fighter>();
        }
    }
}
