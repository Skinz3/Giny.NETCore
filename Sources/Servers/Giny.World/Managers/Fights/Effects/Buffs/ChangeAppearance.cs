using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs
{
    [SpellEffectHandler(EffectsEnum.Effect_ChangeAppearance_335)]
    [SpellEffectHandler(EffectsEnum.Effect_ChangeAppearance)]
    public class ChangeAppearance : SpellEffectHandler
    {
        public ChangeAppearance(EffectDice effect, SpellCastHandler castHandler) :
            base(effect, castHandler)
        {

        }


        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                ServerEntityLook look = SpellAppearanceManager.Instance.Apply(target, Effect.Value);

                if (look != null)
                {
                    int id = target.BuffIdProvider.Pop();
                    LookBuff buff = new LookBuff(id, look, target, this, FightDispellableEnum.REALLY_NOT_DISPELLABLE);
                    target.AddBuff(buff);
                }
            }



        }
    }

}

