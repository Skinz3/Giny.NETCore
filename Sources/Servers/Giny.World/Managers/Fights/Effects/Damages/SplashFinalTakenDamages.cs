using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Damages
{
    /// <summary>
    /// Couronne d'épine.
    /// </summary>
    [SpellEffectHandler(EffectsEnum.Effect_SplashFinalTakenDamages)]
    public class SplashFinalTakenDamages : SpellEffectHandler
    {
        public SplashFinalTakenDamages(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
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
                Damage damages = new Damage(token.Source, target, token.Element, token.BaseMinDamages, token.BaseMaxDamages, token.Handler);
                damages.WontTriggerBuffs = true;
                damages.IgnoreResistances = true;
                damages.IgnoreBoost = true;
                damages.Computed = (short)(token.Computed * (Effect.Min / 100d));
                target.InflictDamage(damages);
            }
        }
    }
}
