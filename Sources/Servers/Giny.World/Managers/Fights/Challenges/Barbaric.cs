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
    /// Barbare
    /// Les personnages alliés doivent achever les ennemis avec une arme.
    /// </summary>
    [Challenge(9)]
    public class Barbaric : Challenge
    {
        public Barbaric(ChallengeRecord record, FightTeam team) : base(record, team)
        {

        }

        public override double XpBonusRatio => 0.10d;

        public override double DropBonusRatio => 0.10;

        public override void BindEvents()
        {
            foreach (var enemy in Team.EnemyTeam.GetFighters<Fighter>())
            {
                enemy.DamageReceived += OnEnemyReceiveDamage;
            }
        }

        private void OnEnemyReceiveDamage(Damage damages, DamageResult result)
        {
            if (!damages.Target.AliveSafe)
            {
                if (!damages.IsWeaponDamage())
                {
                    OnChallengeResulted(ChallengeStateEnum.CHALLENGE_FAILED);
                }
            }
        }

        public override void UnbindEvents()
        {
            foreach (var enemy in Team.EnemyTeam.GetFighters<Fighter>())
            {
                enemy.DamageReceived -= OnEnemyReceiveDamage;
            }
        }

        public override IEnumerable<Fighter> GetAffectedFighters()
        {
            return Team.GetFighters<Fighter>();
        }
    }
}
