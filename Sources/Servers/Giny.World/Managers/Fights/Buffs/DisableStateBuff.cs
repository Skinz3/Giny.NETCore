using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public class DisableStateBuff : Buff
    {
        public short StateId
        {
            get;
            private set;
        }

        private List<StateBuff> DisabledBuffs
        {
            get;
            set;
        }

        public DisableStateBuff(int id, short stateId, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id, target, effectHandler, dispellable, customActionId)
        {
            this.StateId = stateId;
        }

        public override void Execute()
        {
            DisabledBuffs = Target.GetBuffs<StateBuff>().Where(x => x.StateId == StateId).ToList();

            foreach (var buff in DisabledBuffs)
            {
                Target.DisableBuff(buff);
            }
        }

        public override void Dispell()
        {
            foreach (var buff in DisabledBuffs)
            {
                if (Target.HasBuff(buff))
                {
                    Target.EnableBuff(buff);
                }
            }
        }

        public override short GetDelta()
        {
            return StateId;
        }
        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostStateEffect()
            {
                delta = -100,
                dispelable = (byte)Dispellable,
                effectId = Effect.EffectId,
                parentBoostUid = 0,
                spellId = Cast.SpellId,
                stateId = StateId,
                targetId = Target.Id,
                turnDuration = (short)Duration,
                uid = Id,
            };
        }
    }
}
