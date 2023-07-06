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
            var color = 0;

            switch (CastHandler.Cast.Spell.SpellId)
            {
                case 14002: // Rituel de Jashin
                case 12743: // Berserk
                    color = 29761794;
                    break;
            }


            var converted = EntityLookManager.Instance.GetConvertedColor(color, 0);
            foreach (var target in targets) 
            {
                int id = target.BuffIdProvider.Pop();
                var look = target.Look.Clone();
                var colors = look.Colors;
                colors[Effect.Min - 1] = color;
                LookBuff buff = new LookBuff(id, look, target, this, FightDispellableEnum.REALLY_NOT_DISPELLABLE);
                target.AddBuff(buff);
            }
        }
    }
}
