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
     * Vous ne devez jamais finir votre tour sur une cellule adjacente d'un ennemi.
     * L'effet de "Passe ton tour" que certains monstres peuvent infliger ne fait pas échouer ce challenge.
     */
    [Challenge(40)]
    public class Pusillanimous : Challenge
    {
        public Pusillanimous(ChallengeRecord record, FightTeam team) : base(record, team)
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
                    OnChallengeResulted(ChallengeResultEnum.Failed);
                }
            }
        }


        public override IEnumerable<Fighter> GetConcernedFighters()
        {
            return Team.GetFighters<Fighter>();
        }
    }
}
