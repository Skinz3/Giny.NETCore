using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Results;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Other
{
    [SpellEffectHandler(EffectsEnum.Effect_ChangeColor)]
    public class ChangeColor : SpellEffectHandler
    {
        public ChangeColor(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            // expected : 12984578 OR 29761794 (unconverted)

            var color = 0;

            switch (CastHandler.Cast.Spell.SpellId)
            {
                case 12743: // Berserk
                    color = 12984578;
                    break;
            }

        
            foreach (var target in targets) 
            {
                int id = target.BuffIdProvider.Pop();

                var targetColor = (Effect.Min - 1) + 1 << 24 | color & 16777215;

                var look = target.Look.Clone();
                var colors = look.Colors;
                colors[Effect.Min - 1] = targetColor;
                LookBuff buff = new LookBuff(id, look, target, this, FightDispellableEnum.REALLY_NOT_DISPELLABLE);
                target.AddBuff(buff);
            }
        }
    }
}
