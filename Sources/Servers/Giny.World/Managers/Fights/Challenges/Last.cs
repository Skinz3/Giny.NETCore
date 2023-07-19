using Giny.Core.Extensions;
using Giny.Core.Time;
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
    /// <summary>
    /// La cible désignée doit être achevée en dernier.
    /// </summary>
    [Challenge(4)]
    public class Last : Challenge
    {
        public Last(ChallengeRecord record, FightTeam team) : base(record, team)
        {
        }

        public override double XpBonusRatio => 0.3;

        public override double DropBonusRatio => 0.3;

        private Fighter? Target
        {
            get;
            set;
        }
        public override void BindEvents()
        {
            foreach (var enemy in Team.EnemyTeam.GetFighters())
            {
                enemy.Death += OnEnemyDie;
            }
        }


        public override void Initialize()
        {
            base.Initialize();

            Target = Team.EnemyTeam.GetFighters<Fighter>().Random(new AsyncRandom());
            OnTargetUpdated();


        }
        private void OnEnemyDie(Fighter target, Fighter source)
        {
            if (Target == null)
            {
                return;
            }

            if (target == Target && target.Team.Alives >= 1)
            {
                OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
                Target = null;
                OnTargetUpdated();
            }
        }

        public override IEnumerable<Fighter> GetAffectedFighters()
        {
            return Team.GetFighters<Fighter>();
        }

        public override bool IsValid()
        {
            return Team.EnemyTeam.GetFightersCount() > 1;
        }
        public override IEnumerable<Fighter> GetTargets()
        {
            return Target != null ? new Fighter[] { Target } : base.GetTargets();
        }
        public override void UnbindEvents()
        {
            foreach (var enemy in Team.EnemyTeam.GetFighters())
            {
                enemy.Death -= OnEnemyDie;
            }
        }
    }
}
