using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs.SpellBoost
{
    public abstract class SpellBoostBuff : Buff
    {
        protected SpellBoostBuff(int id, short spellId, short delta, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null) : base(id, target, effectHandler, dispellable, customActionId)
        {
            this.SpellId = spellId;
            this.Delta = delta;
        }

        public short SpellId
        {
            get;
            private set;
        }
        private short Delta
        {
            get;
            set;
        }
        public override void Execute()
        {

        }
        public override void Dispell()
        {

        }
        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporarySpellBoostEffect()
            {
                boostedSpellId = SpellId,
                delta = Delta,
                dispelable = (byte)Dispellable,
                effectId =  Effect.EffectId,
                parentBoostUid = 0,
                spellId = Cast.SpellId,
                targetId = Target.Id,
                turnDuration = (short)(Duration == -1 ? -1000 : Duration),
                uid = Id,
            };
        }
        public override short GetDelta()
        {
            return Delta;
        }

    }
}
