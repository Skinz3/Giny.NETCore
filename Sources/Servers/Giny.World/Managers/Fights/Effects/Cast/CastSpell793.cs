using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Cast
{
    [SpellEffectHandler(EffectsEnum.Effect_CastSpell_793)]
    public class CastSpell793 : SpellEffectHandler
    {
        public CastSpell793(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            Spell spell = CreateCastedSpell();

            foreach (var target in targets)
            {
                SpellCast cast = new SpellCast(target, spell, target.Cell, CastHandler.Cast);
                cast.Token = this.GetTriggerToken<ITriggerToken>();
                cast.Force = true;
                cast.Silent = true;
                Source.CastSpell(cast);
            }
        }
    }
}