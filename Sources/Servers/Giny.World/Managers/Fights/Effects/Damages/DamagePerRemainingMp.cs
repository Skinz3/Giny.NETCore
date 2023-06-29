using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Damages
{
    [SpellEffectHandler(EffectsEnum.Effect_DamageNeutralRemainingMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageAirRemainingMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageWaterRemainingMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageFireRemainingMP)]
    [SpellEffectHandler(EffectsEnum.Effect_DamageEarthRemainingMP)]
    public class DamagePerRemainingMp : SpellEffectHandler
    {
        public DamagePerRemainingMp(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override bool Reveals => true;

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                Damage damages = new Damage(Source, target, GetEffectSchool(Effect.EffectEnum), (short)Effect.Min, (short)Effect.Max, this);

                var mp = (double)Source.Stats.MovementPoints.Total();

                double factor = 1;

                if (mp > 0)
                {
                    factor = (mp - Source.Stats.MovementPoints.Used) / mp;
                }


                var testo = damages.BaseMinDamages * factor;

                damages.BaseMaxDamages = (short)(Math.Ceiling(damages.BaseMaxDamages * factor));
                damages.BaseMinDamages = (short)(Math.Ceiling(damages.BaseMinDamages * factor));


                target.InflictDamage(damages);
            }
        }

        private EffectSchoolEnum GetEffectSchool(EffectsEnum effectEnum)
        {
            switch (effectEnum)
            {
                case EffectsEnum.Effect_DamageNeutralRemainingMP:
                    return EffectSchoolEnum.Neutral;
                case EffectsEnum.Effect_DamageAirRemainingMP:
                    return EffectSchoolEnum.Air;
                case EffectsEnum.Effect_DamageWaterRemainingMP:
                    return EffectSchoolEnum.Water;
                case EffectsEnum.Effect_DamageFireRemainingMP:
                    return EffectSchoolEnum.Fire;
                case EffectsEnum.Effect_DamageEarthRemainingMP:
                    return EffectSchoolEnum.Earth;
            }
            return EffectSchoolEnum.Unknown;
        }
    }
}
