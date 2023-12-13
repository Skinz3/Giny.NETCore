using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.IO.D2OClasses;
using Giny.Pokefus.Effects;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Effects.Targets;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Records.Effects;
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
            set
            {
                EffectId = (short)value;  
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

        public FightDispellableEnum DispellableEnum
        {
            get
            {
                return  (FightDispellableEnum)Dispellable;
            }
            set
            {
                Dispellable = (int)value;
            }
        }

        [ProtoMember(17)]
        public string RawZone
        {
            get;
            set;
        }

        private List<Trigger> m_triggers;

        [Annotation]
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
        [Annotation]
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
                case "DCAC":
                    return new Trigger(TriggerTypeEnum.OnDamagedByWeapon);
                case "DM":
                    return new Trigger(TriggerTypeEnum.OnDamagedMelee);
                case "DA":
                    return new Trigger(TriggerTypeEnum.OnDamagedAir);
                case "DF":
                    return new Trigger(TriggerTypeEnum.OnDamagedFire);
                case "DN":
                    return new Trigger(TriggerTypeEnum.OnDamagedNeutral);
                case "DG":
                    return new Trigger(TriggerTypeEnum.OnDamagedByGlyph);
                case "DT":
                    return new Trigger(TriggerTypeEnum.OnDamagedByTrap);
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
                    return new Trigger(TriggerTypeEnum.OnTeleportPortal); 
                case "ATB":
                    return new Trigger(TriggerTypeEnum.AfterTurnBegin);
                case "MPA":
                    return new Trigger(TriggerTypeEnum.OnMpRemovalAttempt);
                case "APA":
                    return new Trigger(TriggerTypeEnum.OnApRemovalAttempt);

                case "CDBE":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageEnnemy);
                case "CDBA":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageAlly);
             
                case "R":
                    return new Trigger(TriggerTypeEnum.OnRangeLost);
                case "CMPA":
                    return new Trigger(TriggerTypeEnum.OnCasterRemoveMpAttempt); 
                case "CAPA":
                    return new Trigger(TriggerTypeEnum.OnCasterRemoveApAttempt); 
                case "CAP":
                    return new Trigger(TriggerTypeEnum.OnCasterUseAp);
                case "MS":
                    return new Trigger(TriggerTypeEnum.OnSwitchPosition);
                case "PPD":
                    return new Trigger(TriggerTypeEnum.OnDamagedByEnemyPush);
                case "CCMPARR":
                    return new Trigger(TriggerTypeEnum.OnAMpUsed);
                case "CMPARR":
                    return new Trigger(TriggerTypeEnum.OnMpUsed);
                case "DCCBA":
                    return new Trigger(TriggerTypeEnum.CasterCriticalHitOnAlly);

                case "CDE":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageEarth);
                case "CDF":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageFire);
                case "CDW":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageWater);
                case "CDA":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageAir);
                case "CDN":
                    return new Trigger(TriggerTypeEnum.CasterInflictDamageNeutral);


                case "V":
                    return new Trigger(TriggerTypeEnum.LifeAffected);
                case "DV":
                    return new Trigger(TriggerTypeEnum.OnDamagedOnLife);
                case "VA":
                    return new Trigger(TriggerTypeEnum.LifePointsAffected);
                case "VM":
                    return new Trigger(TriggerTypeEnum.MaxLifePointsAffected);
                case "VE":
                    return new Trigger(TriggerTypeEnum.ErodedLifePointsAffected);

                case "MA":
                    return new Trigger(TriggerTypeEnum.OnPulled);

                case "K":
                    return new Trigger(TriggerTypeEnum.OnKill);

                case "CS":
                    return new Trigger(TriggerTypeEnum.CasterAddShield);

                case "S":
                    return new Trigger(TriggerTypeEnum.OnShieldApplied);

                case "DTB":
                    return new Trigger(TriggerTypeEnum.OnDamagedTurnBegin);

                case "DTE":
                    return new Trigger(TriggerTypeEnum.OnDamagedTurnEnd);
                
                    /* 
                     * Not sure about how it works
                     * 
                     case "DIS":
                         return new Trigger(TriggerTypeEnum.OnDispelled); 
                     case "CDIS":
                         return new Trigger(TriggerTypeEnum.OnCasterDispelled); 
                     
                     */
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
        public string GetTargetsString()
        {
            string targets = string.Join(",", GetTargets());

            if (targets == string.Empty)
            {
                targets = "ALL";
            }

            return targets;
        }
        public override string ToString()
        {
            return EffectEnum.ToString();
        }
        public abstract ObjectEffect GetObjectEffect();

        public abstract object Clone();

        public bool IsSpellCastEffect()
        {
            switch (EffectEnum)
            {
                case EffectsEnum.Effect_TargetExecuteSpellWithAnimation:
                case EffectsEnum.Effect_TargetExecuteSpell:
                case EffectsEnum.Effect_TargetExecuteSpellOnCell:
                case EffectsEnum.Effect_CasterExecuteSpellGlobalLimitation:
                case EffectsEnum.Effect_CastSpell_1175:
                case EffectsEnum.Effect_CasterExecuteSpell:
                case EffectsEnum.Effect_SourceExecuteSpellOnSource:
                case EffectsEnum.Effect_SourceExecuteSpellOnTarget:

                case EffectsEnum.Effect_TargetExecuteSpellOnSource:
                case EffectsEnum.Effect_TargetExecuteSpellGlobalLimitation:
                case EffectsEnum.Effect_TargetExecuteSpellOnSourceGlobalLimitation:
                case EffectsEnum.Effect_Trap:
                case EffectsEnum.Effect_CasterExecuteSpellOnCell:
                case EffectsEnum.Effect_TargetExecuteSpellWithAnimationGlobalLimitation:
                case EffectsEnum.Effect_TurnBeginGlyph:
                    return true;
            }

            return false;
        }

        public virtual string GetDescription()
        {
            return EffectEnum.ToString();
        }
    }
}
