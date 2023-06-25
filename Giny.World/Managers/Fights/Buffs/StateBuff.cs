using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public class StateBuff : Buff
    {
        public short StateId
        {
            get
            {
                return (short)Record.Id;
            }
        }

        public SpellStateRecord Record
        {
            get;
            private set;
        }
      
        public StateBuff(int id, SpellStateRecord record, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable) : base(id, target, effectHandler, dispellable)
        {
            this.Record = record;
        }

        public override void Execute()
        {
            Target.OnStateAdded(this);
        }

        public override void Dispell()
        {
            Target.OnStateRemoved(this);
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostStateEffect()
            {
                delta = (short)Record.Id,
                dispelable = (byte)Dispellable,
                effectId = Effect.EffectId,
                parentBoostUid = 0,
                spellId = Cast.SpellId,
                stateId = (short)Record.Id,
                targetId = Target.Id,
                turnDuration = (short)(Duration == -1 ? -1000 : Duration),
                uid = Id,
            };
        }

        public override short GetDelta()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "StateBuff : " + Record.Name;
        }
    }
}
