using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Units;
using Giny.World.Records.Challenges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    /// <summary>
    /// Focus
    /// Lorsqu'un ennemi est attaqué par un allié, il doit être achevé avant qu'un autre ennemi ne soit attaqué.
    /// </summary>
    [Challenge(31)]
    public class Focus : Challenge
    {
        public Focus(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.60d;

        public override double DropBonusRatio => 0.60;

        private Fighter? FocusTarget
        {
            get;
            set;
        }

        public override void BindEvents()
        {
            foreach (var fighter in GetAffectedFighters())
            {
                fighter.DamageReceived += OnEnemyReceivedDamage;
            }
        }

        private void OnEnemyReceivedDamage(Damage damages, DamageResult result)
        {
            if (FocusTarget == null || !FocusTarget.AliveSafe)
            {
                FocusTarget = damages.Target;
                OnTargetUpdated();
            }
            else
            {
                if (FocusTarget != damages.Target)
                {
                    OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
                }
                else
                {
                    if (!FocusTarget.AliveSafe)
                    {
                        FocusTarget = null;
                    }
                }

            }
        }
        public override bool IsValid()
        {
            return Team.EnemyTeam.GetFightersCount() > 1;
        }
        public override void UnbindEvents()
        {
            foreach (var fighter in GetAffectedFighters())
            {
                fighter.DamageReceived -= OnEnemyReceivedDamage;
            }
        }

        public override IEnumerable<Fighter> GetTargets()
        {
            if (FocusTarget != null)
            {
                return new Fighter[] { this.FocusTarget };
            }
            else
            {
                return new Fighter[0];
            }
        }
        public override IEnumerable<Fighter> GetAffectedFighters()
        {
            return Team.EnemyTeam.GetFighters<Fighter>();
        }
    }
}
