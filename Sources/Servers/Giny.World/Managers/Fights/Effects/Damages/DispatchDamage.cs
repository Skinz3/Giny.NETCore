using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Formulas;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Damages
{
    /// <summary>
    /// Massacre
    /// </summary>
    [SpellEffectHandler(EffectsEnum.Effect_DispatchDamages)]
    public class DispatchDamage : SpellEffectHandler
    {
        public DispatchDamage(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Damage token = GetTriggerToken<Damage>();

            if (token == null || !token.Computed.HasValue)
            {
                OnTokenMissing<Damage>();
                return;
            }

            foreach (var target in targets)
            {
                Damage damages = new Damage(token.Source, target, token.EffectSchool, token.BaseMinDamages, token.BaseMaxDamages, token.GetEffectHandler());
                damages.WontTriggerBuffs = true;
                damages.IgnoreResistances = true;
                damages.IgnoreBoost = true;
                damages.Computed = (short)(token.Computed * (Effect.Min / 100d));
                target.InflictDamage(damages);
            }

        }
    }
}
