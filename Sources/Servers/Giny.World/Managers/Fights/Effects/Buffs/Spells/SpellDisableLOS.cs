using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects.Buffs.Spells
{
    [SpellEffectHandler(EffectsEnum.Effect_DisableLOS)]
    public class SpellDisableLOS : SpellEffectHandler
    {
        public SpellDisableLOS(EffectDice effect, SpellCastHandler castHandler) : base(effect, castHandler)
        {

        }

        protected override void Apply(IEnumerable<Fighter> targets)
        {
            foreach (var target in targets)
            {
                foreach (var spell in target.GetSpells())
                {
                    int id = target.BuffIdProvider.Pop();
                    Buff buff = new SpellBoostRemoveLOSBuff(id, spell.Id, (short)Effect.Value, target, this, Effect.DispellableEnum,
                     (short)ActionsEnum.ACTION_BOOST_SPELL_NOLINEOFSIGHT);

                    target.AddBuff(buff);
                }
            }
        }
    }
}
