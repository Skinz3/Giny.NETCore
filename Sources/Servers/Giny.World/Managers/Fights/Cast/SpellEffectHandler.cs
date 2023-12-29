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
using Giny.World.Managers.Fights.Zones;
using Giny.Protocol.Custom.Enums;
using Org.BouncyCastle.Asn1.X509;
using Giny.World.Managers.Fights.Effects;
using MySqlX.XDevAPI.Common;
using Giny.Protocol.Messages;

namespace Giny.World.Managers.Fights.Cast
{
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

        protected virtual bool Reveals => false;

        public Zone Zone
        {
            get;
            private set;
        }
        private ITriggerToken? TriggerToken
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
        [Annotation]
        protected bool CasterCriterionSatisfied
        {
            get;
            private set;
        }

        public virtual bool InvertTargetsSort => false;

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

        protected List<Fighter> GetAffectedFighters()
        {
            List<CellRecord> affectedCells = GetAffectedCells();

            /* if (Effect.EffectEnum == EffectsEnum.Effect_Teleport)
             {
                 foreach (var cell in affectedCells)
                 {
                     this.CastHandler.Cast.Source.Fight.Send(new ShowCellMessage(this.Source.Id, cell.Id));
                 }
             } */
            if (Targets.Any(x => x is TargetTypeCriterion && ((TargetTypeCriterion)x).TargetType == SpellTargetType.SELF_ONLY) && !affectedCells.Contains(Source.Cell))
            {
                affectedCells.Add(Source.Cell);
            }

            if (Targets.Any(x => x is LastAttackerCriterion && ((LastAttackerCriterion)x).Required))
            {
                var lastAtkSource = LastAttackerCriterion.GetLastAttackerSource(this);

                if (lastAtkSource.LastAttacker != null && !affectedCells.Contains(lastAtkSource.LastAttacker.Cell))
                    affectedCells.Add(lastAtkSource.LastAttacker.Cell);
            }

            IEnumerable<Fighter> fighters = Source.Fight.GetFighters(affectedCells);

            List<Fighter> results = fighters.Where(entry => entry.Alive && !entry.IsCarried() && IsValidTarget(entry)).ToList();

            results = results.Where(x => !x.IsImmuneToSpell(CastHandler.Cast.SpellId)).ToList();

            SortTargets(results);

            return results;
        }
        protected CellRecord? GetSummonCell()
        {
            var cells = GetAffectedCells().OrderBy(x => x.Point.ManhattanDistanceTo(TargetCell.Point));
            return cells.FirstOrDefault(x => Source.Fight.IsCellFree(x));
        }
        /// <summary>
        /// Tri les cibles de l'effet par cercles concentriques
        /// en partant de π/2
        /// </summary>
        protected void SortTargets(List<Fighter> targets)
        {
            var centerCell = CastHandler.Cast.TargetCell;

            targets.Sort((target1, target2) =>
            {
                var cell1 = target1.Cell;
                var cell2 = target2.Cell;

                var xc1 = cell1.Point.Y - centerCell.Point.Y;
                var yc1 = cell1.Point.X - centerCell.Point.X;

                var xc2 = cell2.Point.Y - centerCell.Point.Y;
                var yc2 = cell2.Point.X - centerCell.Point.X;

                /*
                 * Inversion de la coordonnée y en accord avec les 
                 * convention de dofus pour changer le point de départ 
                 * par rapport au cerle trigonométrique (π/2)
                 */
                double a1 = Math.Atan2(yc1, -xc1);
                double a2 = Math.Atan2(yc2, -xc2);

                /*
                 * Considéré comme une ligne droite
                 */
                if (cell2.Id == centerCell.Id)
                {
                    a2 = a1;
                }
                if (cell1.Id == centerCell.Id)
                {
                    a1 = a2;
                }

                var d1 = cell1.Point.ManhattanDistanceTo(centerCell.Point);

                var d2 = cell2.Point.ManhattanDistanceTo(centerCell.Point);

                if (d1 == d2)
                {
                    return a2.CompareTo(a1);
                }
                else
                {
                    /*
                     * Les effets de poussé appliqueront les effets 
                     * à l'envers par rapport au tri de base
                     */
                    if (InvertTargetsSort)
                    {
                        return cell2.Point.ManhattanDistanceTo(centerCell.Point).CompareTo(cell1.Point.ManhattanDistanceTo(centerCell.Point));
                    }
                    else
                    {
                        return cell1.Point.ManhattanDistanceTo(centerCell.Point).CompareTo(cell2.Point.ManhattanDistanceTo(centerCell.Point));
                    }
                }

            });



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

        [Annotation("usage?")]
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
                    AddTriggerBuff(target, FightDispellableEnum.REALLY_NOT_DISPELLABLE, Trigger.Singleton(TriggerTypeEnum.Delayed), delegate (TriggerBuff buff, ITriggerToken? token)
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
                    AddTriggerBuff(target, FightDispellableEnum.REALLY_NOT_DISPELLABLE, Effect.Triggers, delegate (TriggerBuff buff, ITriggerToken? token)
                    {
                        this.TriggerToken = token;
                        Apply(new Fighter[] { target });
                        return false;

                    }, 0);
                }
            }

        }
        protected abstract void Apply(IEnumerable<Fighter> targets);

        protected SummonedMonster CreateSummon(MonsterRecord record, byte grade, CellRecord cell)
        {
            SummonedMonster fighter = new SummonedMonster(Source, record, this, grade, cell);
            return fighter;
        }



        public T? GetTriggerToken<T>() where T : ITriggerToken
        {
            return (T?)TriggerToken;
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


        protected CharacteristicEnum GetAssociatedCharacteristicEnum()
        {
            return EffectRelator.GetAssociatedCharacteristicEnum(Effect.EffectEnum);
        }
    }

}
