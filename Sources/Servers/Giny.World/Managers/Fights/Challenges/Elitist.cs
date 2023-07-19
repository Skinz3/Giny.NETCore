using Giny.Core.Extensions;
using Giny.Protocol.Enums;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Challenges;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Challenges
{
    /// <summary>
    /// Elitiste
    /// Toutes les attaques doivent être concentrées sur la cible désignée jusqu'à ce qu'il meure.
    /// </summary>
    [Challenge(32)]
    public class Elitist : Challenge
    {
        public Elitist(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.40d;

        public override double DropBonusRatio => 0.40;

        private Fighter? Target
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

        public override void Initialize()
        {
            base.Initialize();

            Target = Team.EnemyTeam.GetFighters<Fighter>().Random(new Random());
            OnTargetUpdated();
        }


        private void OnEnemyReceivedDamage(Damage damages, DamageResult result)
        {
            if (Target == null)
            {
                return;
            }
            if (damages.Target == Target && !Target.AliveSafe)
            {
                Target = null;
                OnChallengeResulted(ChallengeStateEnum.CHALLENGE_COMPLETED);
            }
            else
            {
                if (Target != damages.Target)
                {
                    OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
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
            if (Target != null)
            {
                return new Fighter[] { this.Target };
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
