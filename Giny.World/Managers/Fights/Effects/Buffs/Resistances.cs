using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_SubResistances)]
    [SpellEffectHandler(EffectsEnum.Effect_AddResistances)]
    public class Resistances : SpellEffectHandler
    {
        public Resistances(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {
        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            short delta = 0;

            switch (Effect.EffectEnum)
            {
                case EffectsEnum.Effect_AddResistances:
                    delta = (short)Effect.Min; 
                    break;
                case EffectsEnum.Effect_SubResistances:
                    delta = (short)-Effect.Min;
                    break;
            }

            foreach (var target in targets)
            {
                int id = target.BuffIdProvider.Pop();
                ResistanceBuff buff = new ResistanceBuff(id, delta,  target,
                    this, FightDispellableEnum.DISPELLABLE);
                target.AddBuff(buff);
            }
        }
    }
}
