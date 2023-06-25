using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_DamageWater)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarth)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageAir)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFire)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutral)]
    public class DirectDamage : SpellEffectHandler
    {
        protected override bool Reveals => true;

        public DirectDamage(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var fighter in targets)
            {
                fighter.InflictDamage(CreateDamage(fighter));
            }
        }
        private Damage CreateDamage(Fighter target)
        {
            return new Damage(Source, target, GetEffectSchool(Effect.EffectEnum), (short)Effect.Min, (short)Effect.Max, this);
        }

        public static EffectSchoolEnum GetEffectSchool(EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_DamageWater:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamageFire:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamageAir:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamageEarth:
                    return EffectSchoolEnum.Earth;
                case EffectsEnum.Effect_DamageNeutral:
                    return EffectSchoolEnum.Neutral;

            }
            return EffectSchoolEnum.Unknown;
        }
    }
}
