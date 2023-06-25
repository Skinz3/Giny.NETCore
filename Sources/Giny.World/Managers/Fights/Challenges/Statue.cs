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
     * Les joueurs doivent terminer leur tour sur la case où ils l'ont commencés, les déplacements sont donc possibles lors du tour.
     * Attention à bien terminer votre combat sur la cellule sur laquelle vous avez commencé votre tour.
     */
    [Challenge(2)]
    public class Statue : Challenge
    {
        public Statue(ChallengeRecord record, FightTeam team) : base(record, team)
        {
        }

        public override double XpBonusRatio => 0.80d;

        public override double DropBonusRatio => 0.30d;

        public override void BindEvents()
        {
            Fight.TurnEnded += OnTurnEnded;
        }

        public override void UnbindEvents()
        {
            Fight.TurnEnded -= OnTurnEnded;
        }
        public override IEnumerable<Fighter> GetConcernedFighters()
        {
            return Team.GetFighters<Fighter>();
        }

        private void OnTurnEnded(Fighter fighter)
        {
            if (ConcernedFighters.Contains(fighter))
            {
                if (fighter.TurnStartCell != fighter.Cell)
                {
                    OnChallengeResulted(ChallengeResultEnum.Failed);
                }
            }
        }
    }
}
