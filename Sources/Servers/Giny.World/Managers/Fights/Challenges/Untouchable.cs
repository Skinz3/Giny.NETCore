using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    [Challenge(17)]
    public class Untouchable : Challenge
    {
        public Untouchable(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 1.20;

        public override double DropBonusRatio => 1.20;

        public override void BindEvents()
        {
            foreach (var fighter in GetAffectedFighters())
            {
                fighter.DamageReceived += OnAllyReceiveDamages;
            }
        }

        private void OnAllyReceiveDamages(Damage damages, DamageResult result)
        {
            OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
        }

        public override void UnbindEvents()
        {
            foreach (var fighter in GetAffectedFighters())
            {
                fighter.DamageReceived -= OnAllyReceiveDamages;
            }
        }

        public override IEnumerable<Fighter> GetAffectedFighters()
        {
            return Team.GetFighters<Fighter>();
        }
    }
}
