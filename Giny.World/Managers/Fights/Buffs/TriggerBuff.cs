using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Effects.Damages;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Sequences;
using Giny.World.Managers.Fights.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public class TriggerBuff : Buff
    {
        public delegate bool TriggerBuffApplyHandler(TriggerBuff buff, ITriggerToken token); // return true -> change context behaviour

        public delegate void TriggerBuffRemoveHandler(TriggerBuff buff);

        public IEnumerable<Trigger> Triggers
        {
            get;
            private set;
        }
        public TriggerBuffApplyHandler ApplyTrigger
        {
            get;
            private set;
        }
        public TriggerBuffRemoveHandler RemoveTrigger
        {
            get;
            private set;
        }
        public int Delay
        {
            get;
            set;
        }
        public FightSequence LastTriggeredSequence
        {
            get;
            set;
        }

        public TriggerBuff(int id, IEnumerable<Trigger> triggers, TriggerBuffApplyHandler applyTrigger,
            TriggerBuffRemoveHandler removeTrigger, int delay, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable)
            : base(id, target, effectHandler, dispellable)
        {
            this.Triggers = triggers;
            this.ApplyTrigger = applyTrigger;
            this.Delay = delay;
            this.RemoveTrigger = removeTrigger;
            this.LastTriggeredSequence = null;
        }

        /*
         * Prevent Recursivity
         */
        public bool CanTrigger()
        {
            if (LastTriggeredSequence == null)
            {
                return true;
            }

            // LastTriggeredSequence != this.Target.Fight.SequenceManager.CurrentSequence &&  
            return !LastTriggeredSequence.IsChild(Target.Fight.SequenceManager.CurrentSequence);
        }
        public bool DecrementDelay()
        {
            return (this.Delay -= 1) == 0;
        }
        public override void Execute()
        {
            if (this.ApplyTrigger != null)
            {
                this.ApplyTrigger(this, null);
            }

        }
        public bool Apply(ITriggerToken token)
        {
            if (this.ApplyTrigger != null)
            {
                return this.ApplyTrigger(this, token);
            }

            return false;
        }

        public override void Dispell()
        {
            if (this.RemoveTrigger != null)
            {
                this.RemoveTrigger(this);
            }
        }

        public override IEnumerable<Trigger> GetTriggers()
        {
            return Triggers;
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            int[] values = GetClientParams();
            int param1 = values.Length > 0 ? values[0] : 0;
            int param2 = values.Length > 1 ? values[1] : 0;
            int param3 = values.Length > 2 ? values[2] : 0;

            return new FightTriggeredEffect()
            {
                delay = (short)Delay,
                dispelable = (byte)Dispellable,
                effectId = Effect.EffectId,
                parentBoostUid = 0,
                turnDuration = (short)(Duration == -1 ? -1000 : Duration),
                uid = Id,
                targetId = Target.Id,
                spellId = Cast.SpellId,
                param1 = param1,
                param2 = param2,
                param3 = param3,
            };
        }

        private int[] GetClientParams()
        {
            if (DirectDamage.GetEffectSchool(Effect.EffectEnum) != EffectSchoolEnum.Unknown)
            {
                return new int[] { Effect.Max - Effect.Min, 0, Effect.Min };
            }
            return new int[] { Effect.Min, Effect.Max, Effect.Value };
        }

        public override bool HasDelay()
        {
            return Delay > 0;
        }
        public override short GetDelta()
        {
            throw new InvalidOperationException();
        }
    }
}
