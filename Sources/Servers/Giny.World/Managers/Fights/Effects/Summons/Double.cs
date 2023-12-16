using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Summons
{
    [SpellEffectHandler(EffectsEnum.Effect_DoubleNoSummonSlot)]
    [SpellEffectHandler(EffectsEnum.Effect_Double)]
    public class Double : SpellEffectHandler
    {
        public Double(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {

            bool summonSlot = Effect.EffectEnum == EffectsEnum.Effect_DoubleNoSummonSlot ? false : true;

            SummonedFighter fighter = new DoubleFighter(Source, this, TargetCell, summonSlot);
            Source.Fight.AddSummon(Source, fighter);
        }
    }
}
