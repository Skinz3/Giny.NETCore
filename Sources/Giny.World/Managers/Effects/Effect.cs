using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Pokefus.Effects;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects.Targets;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Records.Maps;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Giny.World.Managers.Effects
{
    [ProtoContract]
    [ProtoInclude(2, typeof(EffectDice))]
    [ProtoInclude(3, typeof(EffectDice))]
    [ProtoInclude(5, typeof(EffectInteger))]
    [ProtoInclude(4, typeof(EffectInteger))]
    [ProtoInclude(18, typeof(EffectPokefus))]
    [ProtoInclude(19, typeof(EffectPokefus))]
    [ProtoInclude(20, typeof(EffectPokefus))]
    [ProtoInclude(21, typeof(EffectPokefusLevel))]
    public abstract class Effect : ICloneable
    {
        public EffectsEnum EffectEnum
        {
            get
            {
                return (EffectsEnum)EffectId;
            }
        }
        [ProtoMember(1)]
        public virtual short EffectId
        {
            get;
            set;
        }
        [ProtoMember(6)]
        public int Order
        {
            get;
            set;
        }
        [ProtoMember(7)]
        public int TargetId
        {
            get;
            set;
        }
        [ProtoMember(8)]
        public string TargetMask
        {
            get;
            set;
        }
        [ProtoMember(9)]
        public int Duration
        {
            get;
            set;
        }
        [ProtoMember(10)]
        public int Delay
        {
            get;
            set;
        }
        [ProtoMember(11)]
        public double Random
        {
            get;
            set;
        }
        [ProtoMember(12)]
        public int Group
        {
            get;
            set;
        }
        [ProtoMember(13)]
        public int Modificator
        {
            get;
            set;
        }
        [ProtoMember(14)]
        public bool Trigger
        {
            get;
            set;
        }
        [ProtoMember(15)]
        public string RawTriggers
        {
            get;
            set;
        }
        [ProtoMember(16)]
        public int Dispellable
        {
            get;
            set;
        }
        [ProtoMember(17)]
        public string RawZone
        {
            get;
            set;
        }

        private List<Trigger> m_triggers;

        [WIP]
        public List<Trigger> Triggers
        {
            get
            {
                if (m_triggers == null)
                {
                    m_triggers = ParseTriggers();
                }

                return ParseTriggers(); // m_triggers
            }
        }

        public Effect()
        {

        }

        public Effect(short effectId)
        {
            this.EffectId = effectId;
        }

        public Zone GetZone()
        {
            return GetZone(0);
        }
        public Zone GetZone(DirectionsEnum direction)
        {
            return ZoneManager.Instance.BuildZone(RawZone, direction);
        }
        [WIP]
        private Trigger ParseTrigger(string input)
        {
            string identifier = input.RemoveNumbers();

            string rawParameter = input.RemoveLetters();

            int parameter = 0;

            if (rawParameter != string.Empty)
            {
                parameter = int.Parse(rawParameter);
            }

            switch (identifier)
            {
                case "PT":
                    return new Trigger(TriggerTypeEnum.OnTeleportPortal);
                case "CT":
                    return new Trigger(TriggerTypeEnum.OnTackle);
                case "CI":
                    return new Trigger(TriggerTypeEnum.OnSummon);
                case "H":
                    return new Trigger(TriggerTypeEnum.OnHealed);
                case "P":
                    return new Trigger(TriggerTypeEnum.OnPushed);
                case "TE":
                    return new Trigger(TriggerTypeEnum.OnTurnEnd);
                case "TB":
                    return new Trigger(TriggerTypeEnum.OnTurnBegin);
                case "DI":
                    return new Trigger(TriggerTypeEnum.OnDamagedBySummon);
                case "D":
                    return new Trigger(TriggerTypeEnum.OnDamaged);
                case "DR":
                    return new Trigger(TriggerTypeEnum.OnDamagedRange);
                case "DS":
                    return new Trigger(TriggerTypeEnum.OnDamagedBySpell);
                case "DM":
                    return new Trigger(TriggerTypeEnum.OnDamagedMelee);
                case "DA":
                    return new Trigger(TriggerTypeEnum.OnDamagedAir);
                case "DF":
                    return new Trigger(TriggerTypeEnum.OnDamagedFire);
                case "DN":
                    return new Trigger(TriggerTypeEnum.OnDamagedNeutral);
                case "DE":
                    return new Trigger(TriggerTypeEnum.OnDamagedEarth);
                case "DW":
                    return new Trigger(TriggerTypeEnum.OnDamagedWater);
                case "PD":
                    return new Trigger(TriggerTypeEnum.OnDamagedByPush);
                case "PMD":
                    return new Trigger(TriggerTypeEnum.OnDamagedByAllyPush);
                case "DBE":
                    return new Trigger(TriggerTypeEnum.OnDamagedByEnemy);
                case "DBA":
                    return new Trigger(TriggerTypeEnum.OnDamagedByAlly);
                case "CDM":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageMelee);
                case "CDR":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageRange);
                case "CC":
                    return new Trigger(TriggerTypeEnum.OnCriticalHit);
                case "M":
                    return new Trigger(TriggerTypeEnum.OnMoved);
                case "X":
                    return new Trigger(TriggerTypeEnum.OnDeath);
                case "I":
                    return new Trigger(TriggerTypeEnum.Instant);
                case "EON":
                    return new Trigger(TriggerTypeEnum.OnStateAdded, parameter);
                case "EOFF":
                    return new Trigger(TriggerTypeEnum.OnStateRemoved, parameter);
                case "TP":
                    return new Trigger(TriggerTypeEnum.OnTeleportPortal); // <---- TODO
                case "ATB":
                    return new Trigger(TriggerTypeEnum.AfterTurnBegin);
                case "MPA":
                    return new Trigger(TriggerTypeEnum.OnMPLost);
                case "APA":
                    return new Trigger(TriggerTypeEnum.OnAPLost);
                case "CDBE":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageEnnemy);
                case "V":
                    return new Trigger(TriggerTypeEnum.OnLifePointsPending);
                case "R":
                    return new Trigger(TriggerTypeEnum.OnRangeLost);
            }

            return new Trigger(TriggerTypeEnum.Unknown);
        }

        private List<Trigger> ParseTriggers()
        {
            List<Trigger> results = new List<Trigger>();

            if (RawTriggers == string.Empty)
            {
                return results;
            }

            const char TriggerSplitter = '|';

            foreach (var rawTrigger in RawTriggers.Split(TriggerSplitter))
            {
                Trigger trigger = ParseTrigger(rawTrigger);
                results.Add(trigger);
            }

            return results;
        }
        public IEnumerable<TargetCriterion> GetTargets()
        {
            if (string.IsNullOrEmpty(TargetMask) || TargetMask == "a,A" || TargetMask == "A,a")
            {
                return new TargetCriterion[0]; // default target = ALL
            }

            var data = TargetMask.Split(',');

            IEnumerable<TargetCriterion> result = data.Select(TargetCriterion.ParseCriterion).ToArray();

            return result;
        }

        public override string ToString()
        {
            return EffectEnum.ToString();
        }
        public abstract ObjectEffect GetObjectEffect();

        public abstract object Clone();

    }
}
