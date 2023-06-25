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
     *  Il est l'opposé du challenge Barbare, 
     *  vous ne devez jamais utiliser votre arme de corps à corps
     *  de tout le combat et n'utiliser que vos sorts
     *  */
    [Challenge(11)]
    public class Mystical : Challenge
    {
        public Mystical(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.70;

        public override double DropBonusRatio => 0.30;

        public override void BindEvents()
        {
            foreach (var fighter in ConcernedFighters.OfType<CharacterFighter>())
            {
                fighter.OnCloseCombat += OnCloseCombat;
            }
        }

        public override IEnumerable<Fighter> GetConcernedFighters()
        {
            return Team.GetFighters<CharacterFighter>();
        }

        public override void UnbindEvents()
        {
            foreach (var fighter in ConcernedFighters.OfType<CharacterFighter>())
            {
                fighter.OnCloseCombat -= OnCloseCombat;
            }
        }

        private void OnCloseCombat(CharacterFighter fighter)
        {
            OnChallengeResulted(ChallengeResultEnum.Failed);
        }

    }
}
