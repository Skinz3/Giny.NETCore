using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Buffs
{
    public abstract class Buff : ITriggerToken
    {
        public int Id
        {
            get;
            set;
        }
        public SpellCast Cast
        {
            get
            {
                return EffectHandler.CastHandler.Cast;
            }
        }
        public Fighter Target
        {
            get;
            private set;
        }
        private SpellEffectHandler EffectHandler
        {
            get;
            set;
        }
        public EffectDice Effect
        {
            get
            {
                return EffectHandler.Effect;
            }
        }
        public FightDispellableEnum Dispellable
        {
            get;
            private set;
        }
        private short? CustomActionId
        {
            get;
            set;
        }
        public int Duration
        {
            get;
            set;
        }
        public int TurnIndex
        {
            get;
            set;
        }
        public bool Applied
        {
            get;
            private set;
        }
        public Buff(int id, Fighter target, SpellEffectHandler effectHandler, FightDispellableEnum dispellable, short? customActionId = null)
        {
            this.Id = id;
            this.Target = target;
            this.EffectHandler = effectHandler;
            this.TurnIndex = target.Fight.GetTurnIndex();
            this.Duration = effectHandler.Effect.Duration;
            this.Dispellable = dispellable;
            this.CustomActionId = customActionId;

            if (Duration > 60) // fuck ankama.
            {
                Duration = -1;
            }
        }

        public bool DecrementDuration()
        {
            return this.Duration != -1 && (this.Duration -= 1) <= 0;
        }

        public void Apply()
        {
            Execute();
            this.Applied = true;
        }

        public abstract void Execute();

        public abstract void Dispell();

        public abstract short GetDelta();

        public short GetActionId()
        {
            return CustomActionId.HasValue ? CustomActionId.Value : Effect.EffectId;
        }

        public virtual AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            return new FightTemporaryBoostEffect()
            {
                delta = Math.Abs(GetDelta()),
                dispelable = (byte)Dispellable,
                turnDuration = (short)Duration,
                effectId = Effect.EffectId,
                parentBoostUid = 0,
                spellId = Cast.SpellId,
                targetId = Target.Id,
                uid = Id,
            };
        }

        public FightDispellableEffectExtendedInformations GetFightDispellableEffectExtendedInformations() => new FightDispellableEffectExtendedInformations(GetActionId(), Cast.Source.Id, GetAbstractFightDispellableEffect());

        public virtual IEnumerable<Trigger> GetTriggers()
        {
            return Trigger.Singleton(TriggerTypeEnum.Instant);
        }
        public virtual bool HasDelay()
        {
            return false;
        }

        public Fighter GetSource()
        {
            return this.Cast.Source;
        }
    }
}
