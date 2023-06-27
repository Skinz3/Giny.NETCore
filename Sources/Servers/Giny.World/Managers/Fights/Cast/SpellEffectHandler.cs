using Giny.World.Managers.Fights;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giny.World.Managers.Effects.Targets;
using Giny.Core.DesignPattern;
using Giny.IO.D2OTypes;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Effects;
using Giny.World.Records.Effects;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Stats;
using Giny.Protocol.Enums;
using Giny.World.Records.Monsters;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Managers.Actions;
using Giny.World.Managers.Fights.Zones;
using Giny.Protocol.Custom.Enums;
using Org.BouncyCastle.Asn1.X509;

namespace Giny.World.Managers.Fights.Cast
{
    [WIP("dispellable should not be constants.")]
    public abstract class SpellEffectHandler
    {
        public Fighter Source
        {
            get
            {
                return CastHandler.Cast.Source;
            }
        }
        protected CellRecord CastCell
        {
            get
            {
                return CastHandler.Cast.CastCell;
            }
        }
        protected CellRecord TargetCell
        {
            get
            {
                return CastHandler.Cast.TargetCell;
            }
        }
        public EffectDice Effect
        {
            get;
            private set;
        }
        protected bool Critical
        {
            get
            {
                return CastHandler.Cast.IsCriticalHit;
            }
        }
        public IEnumerable<TargetCriterion> Targets
        {
            get;
            set;
        }
        public SpellCastHandler CastHandler
        {
            get;
            private set;
        }
        public bool IsCastByPortal
        {
            get;
            set;
        } = false;

        protected virtual bool Reveals => false;

        public Zone Zone
        {
            get;
            private set;
        }
        private ITriggerToken TriggerToken
        {
            get;
            set;
        }
        private IEnumerable<Fighter> AffectedFighters
        {
            get;
            set;
        }
        /*
         * Sorts d'invocations avec TargetMask
         * Karcham & Chamrak
         */
        [WIP]
        protected bool CasterCriterionSatisfied
        {
            get;
            private set;
        }
        public SpellEffectHandler(EffectDice effect, SpellCastHandler castHandler)
        {
            Targets = effect.GetTargets();

            if (Targets.Any(x => x is UnknownCriterion))
            {
                castHandler.Cast.Source.Fight.Warn("Unknown Target Mask : " + effect.TargetMask);
            }
            this.CastHandler = castHandler;
            Effect = effect;
            Zone = Effect.GetZone(CastCell.Point.OrientationTo(TargetCell.Point));

            this.AffectedFighters = GetAffectedFighters();

            this.CasterCriterionSatisfied = ComputeCasterCriterion();



        }
        private bool ComputeCasterCriterion()
        {
            var caster = Source;
            bool result = Targets.OfType<StateCriterion>().Where(x => x.Caster).All(x => x.IsTargetValid(caster, this));
            bool result2 = Targets.OfType<CanSummonCriterion>().Where(x => x.Caster).All(x => x.IsTargetValid(caster, this));
            return result & result2;
        }

        private IEnumerable<Fighter> GetAffectedFighters()
        {

            List<CellRecord> affectedCells = GetAffectedCells();

            /* foreach (var cell in affectedCells)
             {
                 Source.Fight.Send(new Giny.Protocol.Messages.ShowCellMessage(cell.Id, cell.Id));
             } */


            if (Targets.Any(x => x is TargetTypeCriterion && ((TargetTypeCriterion)x).TargetType == SpellTargetType.SELF_ONLY) && !affectedCells.Contains(Source.Cell))
                affectedCells.Add(Source.Cell); // Source.Cell

            var fighters = Source.Fight.GetFighters(affectedCells);

            var results = fighters.Where(entry => entry.Alive && !entry.IsCarried() && IsValidTarget(entry)).ToArray();

            return results;
        }

        public bool IsValidTarget(Fighter actor)
        {
            var targets = Targets.ToLookup(x => x.GetType());

            return targets.All(x => x.First().IsDisjonction ?
               x.Any(y => y.IsTargetValid(actor, this)) : x.All(y => y.IsTargetValid(actor, this)));
        }
        protected List<CellRecord> GetAffectedCells()
        {
            return Zone.GetCells(TargetCell, CastCell, Source.Fight.Map).ToList();
        }

        [WIP("usage?")]
        public virtual bool CanApply()
        {
            return true;
        }

        protected Spell CreateCastedSpell()
        {
            SpellRecord spellRecord = SpellRecord.GetSpellRecord((short)Effect.Min);

            if (spellRecord == null)
            {
                Source.Fight.Warn("Unable to create spell : " + Effect.Min + "...");
                return null;
            }
            SpellLevelRecord level = spellRecord.GetLevel((byte)Effect.Max);
            return new Spell(spellRecord, level);
        }
        public bool RevealsInvisible()
        {
            return Reveals && Trigger.IsInstant(Effect.Triggers);
        }
        public void Execute()
        {


            if (!CasterCriterionSatisfied)
            {
                return;
            }

            if (Targets.Any(x => x.RefreshTargets))
            {
                AffectedFighters = GetAffectedFighters();
            }

            Execute(AffectedFighters);
        }
        public void Execute(IEnumerable<Fighter> targets)
        {
            if (Effect.Triggers.Any(x => x.Type == TriggerTypeEnum.Unknown))
            {
                Source.Fight.Warn("Unknown trigger(s) : " + Effect.RawTriggers + " cannot cast effect " + Effect.EffectEnum);
                return;
            }

            if (Effect.Delay > 0)
            {
                foreach (var target in targets)
                {
                    AddTriggerBuff(target, FightDispellableEnum.REALLY_NOT_DISPELLABLE, Trigger.Singleton(TriggerTypeEnum.Delayed), delegate (TriggerBuff buff, ITriggerToken token)
                      {
                          InternalApply(new Fighter[] { target });
                          return false;

                      }, (short)Effect.Delay);
                }

            }
            else
            {
                InternalApply(targets);
            }

        }
        private void InternalApply(IEnumerable<Fighter> targets)
        {
            if (Trigger.IsInstant(Effect.Triggers))
            {
                Apply(targets);
            }
            else
            {
                foreach (var target in targets)
                {
                    AddTriggerBuff(target, FightDispellableEnum.REALLY_NOT_DISPELLABLE, Effect.Triggers, delegate (TriggerBuff buff, ITriggerToken token)
                    {
                        this.TriggerToken = token;
                        Apply(new Fighter[] { target });
                        return false;

                    }, 0);
                }
            }

        }
        protected abstract void Apply(IEnumerable<Fighter> targets);

        protected SummonedMonster CreateSummon(MonsterRecord record, byte grade)
        {
            SummonedMonster fighter = new SummonedMonster(Source, record, this, grade, TargetCell);
            return fighter;
        }


        public T GetTriggerToken<T>() where T : ITriggerToken
        {
            return (T)TriggerToken;
        }
        public void SetTriggerToken(ITriggerToken token)
        {
            TriggerToken = token;
        }
        protected TriggerBuff AddTriggerBuff(Fighter target, FightDispellableEnum dispellable, IEnumerable<Trigger> triggers, TriggerBuff.TriggerBuffApplyHandler applyTrigger,
            short delay)
        {
            return AddTriggerBuff(target, dispellable, triggers, applyTrigger, null, delay);
        }

        protected TriggerBuff AddTriggerBuff(Fighter target, FightDispellableEnum dispellable, IEnumerable<Trigger> triggers, TriggerBuff.TriggerBuffApplyHandler applyTrigger)
        {
            return AddTriggerBuff(target, dispellable, triggers, applyTrigger, 0);
        }
        protected TriggerBuff AddTriggerBuff(Fighter target, FightDispellableEnum dispellable, IEnumerable<Trigger> triggers, TriggerBuff.TriggerBuffApplyHandler applyTrigger,
           TriggerBuff.TriggerBuffRemoveHandler removeTrigger, short delay)
        {
            int id = target.BuffIdProvider.Pop();
            TriggerBuff triggerBuff = new TriggerBuff(id, triggers, applyTrigger, removeTrigger, delay, target, this, dispellable);
            target.AddBuff(triggerBuff);
            return triggerBuff;
        }
        public StatBuff AddStatBuff(Fighter target, short value, Characteristic characteristic, FightDispellableEnum dispellable, short? customActionId = null)
        {
            int id = target.BuffIdProvider.Pop();
            StatBuff statBuff = new StatBuff(id, target, this, Critical, dispellable, characteristic, value, customActionId);
            target.AddBuff(statBuff);
            return statBuff;
        }
        public StateBuff AddStateBuff(Fighter target, SpellStateRecord record, FightDispellableEnum dispellable)
        {
            int id = target.BuffIdProvider.Pop();
            StateBuff buff = new StateBuff(id, record, target, this, dispellable);
            target.AddBuff(buff);
            return buff;
        }
        public StateBuff AddStateBuff(Fighter target, SpellStateRecord record, FightDispellableEnum dispellable, short duration)
        {
            int id = target.BuffIdProvider.Pop();
            StateBuff buff = new StateBuff(id, record, target, this, dispellable);
            buff.Duration = duration;
            target.AddBuff(buff);
            return buff;
        }
        public VitalityBuff AddVitalityBuff(Fighter target, short delta, FightDispellableEnum dispellable, ActionsEnum actionId)
        {
            int id = target.BuffIdProvider.Pop();
            VitalityBuff buff = new VitalityBuff(id, delta, target, this, dispellable, actionId);
            target.AddBuff(buff);
            return buff;
        }
        public void OnTokenMissing<T>() where T : ITriggerToken
        {
            Source.Fight.Warn("Unable to compute effect (" + Effect.EffectEnum + "). Token is missing (" + typeof(T).Name + ")");
        }
        public override string ToString()
        {
            return Effect.EffectEnum + " Z:" + Effect.RawZone + " TM:" + Effect.TargetMask + " TRIG:" + Effect.RawTriggers;
        }


        protected CharacteristicEnum GetAssociatedCharacteristic(EffectsEnum effect)
        {
            switch (effect)
            {
                case EffectsEnum.Effect_AddChance:
                case EffectsEnum.Effect_StealChance:
                    return CharacteristicEnum.CHANCE;

                case EffectsEnum.Effect_StealWisdom:
                    return CharacteristicEnum.WISDOM;

                case EffectsEnum.Effect_AddIntelligence:
                case EffectsEnum.Effect_StealIntelligence:
                    return CharacteristicEnum.INTELLIGENCE;

                case EffectsEnum.Effect_StealAgility:
                case EffectsEnum.Effect_AddAgility:
                    return CharacteristicEnum.AGILITY;

                case EffectsEnum.Effect_AddStrength:
                case EffectsEnum.Effect_StealStrength:
                    return CharacteristicEnum.STRENGTH;

                case EffectsEnum.Effect_AddRange:
                case EffectsEnum.Effect_AddRange_136:
                case EffectsEnum.Effect_StealRange:
                    return CharacteristicEnum.RANGE;

                case EffectsEnum.Effect_IncreaseDamage_138:
                    return CharacteristicEnum.DAMAGE_PERCENT;



                case EffectsEnum.Effect_AddWisdom:
                    return CharacteristicEnum.WISDOM;

                case EffectsEnum.Effect_AddWeaponDamageBonus:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON;

                case EffectsEnum.Effect_MeleeDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_MELEE;

                case EffectsEnum.Effect_RangedDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_DISTANCE;

                case EffectsEnum.Effect_AddDamageBonus:
                    return CharacteristicEnum.ALL_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddCriticalHit:
                    return CharacteristicEnum.CRITICAL_HIT;

                case EffectsEnum.Effect_AddDodgeMPProbability:
                    return CharacteristicEnum.DODGE_MP_LOST_PROBABILITY;

                case EffectsEnum.Effect_AddDodgeAPProbability:
                    return CharacteristicEnum.DODGE_AP_LOST_PROBABILITY;

                case EffectsEnum.Effect_AddMeleeResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_MELEE;

                case EffectsEnum.Effect_AddRangedResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_DISTANCE;

                case EffectsEnum.Effect_AddSpellResistance:
                    return CharacteristicEnum.RECEIVED_DAMAGE_MULTIPLIER_SPELLS;

                case EffectsEnum.Effect_AddLock:
                    return CharacteristicEnum.TACKLE_BLOCK;

                case EffectsEnum.Effect_AddWaterDamageBonus:
                    return CharacteristicEnum.WATER_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddFireDamageBonus:
                    return CharacteristicEnum.FIRE_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddAirDamageBonus:
                    return CharacteristicEnum.AIR_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddEarthDamageBonus:
                    return CharacteristicEnum.EARTH_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddNeutralDamageBonus:
                    return CharacteristicEnum.NEUTRAL_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddAPAttack:
                    return CharacteristicEnum.AP_REDUCTION;

                case EffectsEnum.Effect_AddMPAttack:
                    return CharacteristicEnum.MP_REDUCTION;

                case EffectsEnum.Effect_AddFireResistPercent:
                    return CharacteristicEnum.FIRE_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddWaterResistPercent:
                    return CharacteristicEnum.WATER_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddEarthResistPercent:
                    return CharacteristicEnum.EARTH_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddAirResistPercent:
                    return CharacteristicEnum.AIR_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddNeutralResistPercent:
                    return CharacteristicEnum.NEUTRAL_ELEMENT_RESIST_PERCENT;

                case EffectsEnum.Effect_AddEvade:
                    return CharacteristicEnum.TACKLE_EVADE;

                case EffectsEnum.Effect_AddDamageBonusPercent:
                    return CharacteristicEnum.DAMAGE_PERCENT;

                case EffectsEnum.Effect_AddNeutralElementReduction:
                    return CharacteristicEnum.NEUTRAL_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddFireElementReduction:
                    return CharacteristicEnum.FIRE_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddAirElementReduction:
                    return CharacteristicEnum.AIR_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddEarthElementReduction:
                    return CharacteristicEnum.EARTH_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddWaterElementReduction:
                    return CharacteristicEnum.WATER_ELEMENT_REDUCTION;

                case EffectsEnum.Effect_AddTrapBonusPercent:
                    return CharacteristicEnum.TRAP_DAMAGE_BONUS_PERCENT;

                case EffectsEnum.Effect_WeaponDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_WEAPON;

                case EffectsEnum.Effect_AddPushDamageReduction:
                    return CharacteristicEnum.PUSH_DAMAGE_REDUCTION;

                case EffectsEnum.Effect_AddTrapBonus:
                    return CharacteristicEnum.TRAP_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddHealBonus:
                    return CharacteristicEnum.HEAL_BONUS;

                case EffectsEnum.Effect_AddPushDamageBonus:
                    return CharacteristicEnum.PUSH_DAMAGE_BONUS;

                case EffectsEnum.Effect_AddCriticalDamageReduction:
                    return CharacteristicEnum.CRITICAL_DAMAGE_REDUCTION;

                case EffectsEnum.Effect_AddCriticalDamageBonus:
                    return CharacteristicEnum.CRITICAL_DAMAGE_BONUS;

                case EffectsEnum.Effect_SpellDamageDonePercent:
                    return CharacteristicEnum.DEALT_DAMAGE_MULTIPLIER_SPELLS;

                case EffectsEnum.Effect_IncreaseSpellDamage:
                    return CharacteristicEnum.DAMAGE_PERCENT_SPELL;
            }

            throw new NotImplementedException("Unknown corresponding characteristic for effect " + effect);
        }
    }
}
