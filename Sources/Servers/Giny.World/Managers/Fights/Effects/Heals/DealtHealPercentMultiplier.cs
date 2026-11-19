using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Units;
using System;
using System.Collections.Generic;

namespace Giny.World.Managers.Fights.Effects.Heals
{
    [SpellEffectHandler(EffectsEnum.Effect_AddDealtHealPercentMultiplier)]
    public class AddDealtHealPercentMultiplierHandlerEmpty : SpellEffectHandler
    {
        public AddDealtHealPercentMultiplierHandlerEmpty(EffectDice effect, SpellCastHandler castHandler)
            : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            // Handler vide, ne fait rien.
            // Sert uniquement à éviter le NullPointerException si cet effet est rencontré.
            //TODO
            //Théoriquement c'est l'effet 2971 dans les D2O
            // Augmente les soins finaux occasionnés de (XX) %
        }
    }
}
