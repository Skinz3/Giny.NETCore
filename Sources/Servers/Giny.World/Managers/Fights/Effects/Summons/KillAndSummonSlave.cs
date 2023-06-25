using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Summons
{
    [SpellEffectHandler(EffectsEnum.Effect_KillAndSummonSlave)]
    public class KillAndSummonSlave : SpellEffectHandler
    {
        public KillAndSummonSlave(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            if (!(Source is CharacterFighter))
            {
                return;
            }

            foreach (var target in targets)
            {
                target.Die(Source);

                MonsterRecord record = MonsterRecord.GetMonsterRecord((short)Effect.Min);

                if (record != null && Source.Fight.IsCellFree(TargetCell))
                {
                    SummonedMonster summon = CreateSummon(record, (byte)Effect.Max);

                    if (Source.CanSummon() || !summon.UseSummonSlot())
                    {
                        summon.SetController((CharacterFighter)Source);
                        Source.Fight.AddSummon(Source, summon);
                    }
                }
            }
        }
    }
}
