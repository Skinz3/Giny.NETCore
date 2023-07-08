using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Pool;
using Giny.Core.Time;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Api;
using Giny.World.Handlers.Roleplay.Maps.Paths;
using Giny.World.Managers.Entities.Look;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Buffs.SpellBoost;
using Giny.World.Managers.Fights.Buffs.SpellModification;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Effects.Damages;
using Giny.World.Managers.Fights.History;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Fights.Movements;
using Giny.World.Managers.Fights.Results;
using Giny.World.Managers.Fights.Sequences;
using Giny.World.Managers.Fights.Stats;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Managers.Fights.Zones.Sets;
using Giny.World.Managers.Maps;
using Giny.World.Managers.Spells;
using Giny.World.Managers.Stats;
using Giny.World.Records.Maps;
using Giny.World.Records.Spells;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using Spell = Giny.World.Managers.Fights.Cast.Spell;

namespace Giny.World.Managers.Fights.Fighters
{
    public abstract class Fighter
    {
        public const short CarrySpellState = 3;

        public const short CarriedSpellState = 8;

        public const short GravityState = 7;

        public delegate void FighterEventDelegate(Fighter target);

        public delegate void FighterKilledDelegate(Fighter target, Fighter source);

        public event FighterEventDelegate Moved;

        public event FighterEventDelegate Tackled;

        public event FighterKilledDelegate Killed;

        public int Id
        {
            get;
            set;
        }
        public abstract string Name
        {
            get;
        }
        public bool Alive
        {
            get;
            set;
        } = true;

        public Fight Fight
        {
            get;
            set;
        }
        public FightTeam Team
        {
            get;
            set;
        }
        public FightTeam EnemyTeam
        {
            get
            {
                return Team == Fight.BlueTeam ? Fight.RedTeam : Fight.BlueTeam;
            }
        }

        public CellRecord Cell
        {
            get;
            protected set;
        }


        public CellRecord TurnStartCell
        {
            get;
            set;
        }
        public DirectionsEnum Direction
        {
            get;
            protected set;
        }
        public ServerEntityLook Look
        {
            get;
            set;
        }
        public ServerEntityLook BaseLook
        {
            get;
            private set;
        }
        public FighterStats Stats
        {
            get;
            set;
        }
        public DateTime? DeathTime
        {
            get;
            set;
        }
        public bool IsReady
        {
            get;
            set;
        } = false;

        public abstract short Level
        {
            get;
        }

        public abstract BreedEnum Breed
        {
            get;
        }
        public CellRecord FightStartCell
        {
            get;
            protected set;
        }
        public CellRecord RoleplayCell
        {
            get;
            set;
        }

        private List<Buff> Buffs
        {
            get;
            set;
        }
        public UniqueIdProvider BuffIdProvider
        {
            get;
            private set;
        }

        public bool IsFighterTurn => Fight.FighterPlaying == this;

        public bool Left
        {
            get;
            set;
        }
        public Loot Loot
        {
            get;
            private set;
        }

        public Fighter Carried
        {
            get;
            private set;
        }

        public virtual bool CanDrop => false;

        public SpellHistory SpellHistory
        {
            get;
            private set;
        }
        public MovementHistory MovementHistory
        {
            get;
            private set;
        }
        public Fighter LastAttacker
        {
            get;
            private set;
        }
        public abstract bool Sex
        {
            get;
        }

        /// <summary>
        /// (From client SpellManager.as:352)
        /// True if the player was teleported in invalid cell during his turn.
        /// (Pendule xelor)
        /// </summary>
        public bool WasTeleportedInInvalidCell
        {
            get;
            private set;
        }
        /*
         * Prevent some Telefrag recursive issues (Desynchronisaiton)
         */
        [WIP] // work on sram traps and test again desynchronisation
        public CellRecord LastExchangedPositionSequenced
        {
            get;
            set;
        }

        public int TotalDamageReceivedSequenced
        {
            get;
            set;
        }

        public Random Random
        {
            get;
            private set;
        }

        public SpellModifiers SpellModifiers
        {
            get;
            private set;
        }
        /// <summary>
        /// Unused?
        /// </summary>
        private bool IsMoving
        {
            get;
            set;
        }
        public Fighter(FightTeam team, CellRecord roleplayCell)
        {
            this.Team = team;
            this.RoleplayCell = roleplayCell;
            this.Cell = Team.GetPlacementCell();
            this.Loot = new Loot();
            this.Buffs = new List<Buff>();
            this.BuffIdProvider = new UniqueIdProvider();
            this.SpellHistory = new SpellHistory(this);
            this.SpellModifiers = new SpellModifiers(this);
            this.WasTeleportedInInvalidCell = false;
            this.Random = new Random();
        }

        public virtual void Initialize()
        {
            this.TurnStartCell = this.Cell;
            this.MovementHistory = new MovementHistory(this);
            this.BaseLook = Look.Clone();

            foreach (var stat in Stats.GetCharacteristics<Characteristic>())
            {
                stat.Value.OnContextChanged += ((Characteristic characteristic) =>
                {
                    this.RefreshStats(stat.Key);
                });
            }

        }



        public void FindPlacementDirection()
        {
            Tuple<short, short> tuple = null;

            foreach (Fighter current in this.EnemyTeam.GetFighters<Fighter>())
            {
                MapPoint point = current.Cell.Point;
                if (tuple == null)
                {
                    tuple = Tuple.Create(current.Cell.Id, this.Cell.Point.ManhattanDistanceTo(point));
                }
                else
                {
                    if (this.Cell.Point.ManhattanDistanceTo(point) < tuple.Item2)
                    {
                        tuple = Tuple.Create(current.Cell.Id, this.Cell.Point.ManhattanDistanceTo(point));
                    }
                }
            }
            if (tuple == null)
            {
                this.Direction = DirectionsEnum.DIRECTION_SOUTH_WEST;
            }
            else
            {
                this.Direction = this.Cell.Point.OrientationTo(new MapPoint((short)tuple.Item1), false);
            }
        }



        public virtual IEnumerable<DroppedItem> RollLoot(IFightResult looter, double bonusRatio)
        {
            return new DroppedItem[0];
        }

        public virtual void OnMoveFailed(MovementFailedReason reason)
        {

        }
        protected virtual void OnTackled(short looseMp, short looseAp, IEnumerable<Fighter> tacklers)
        {
            Fight.Send(new GameActionFightTackledMessage()
            {
                actionId = 0,
                sourceId = this.Id,
                tacklersIds = tacklers.Select(x => (double)x.Id).ToArray(),
            });

            this.LooseAp(this, looseAp, ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_LOST);
            this.LooseMp(this, looseMp, ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_LOST);

            foreach (var tackler in tacklers)
            {
                /*
                 * Only tacklers that really tackle should trigger buffs? (not sure) *
                 * Also we should pass Tackle.cs as token
                 */
                tackler.TriggerBuffs(TriggerTypeEnum.OnTackle, null);
            }

            Tackled?.Invoke(this);
        }
        private List<CellRecord> ApplyTackle(List<CellRecord> path)
        {
            if (!CanBeTackled() || IsInvisible())
            {
                return path;
            }

            IEnumerable<Fighter> tacklers = GetTacklers(Cell);

            Tackle tackle = GetTackle(tacklers);

            if (tackle.ApLoss > 0 || tackle.MpLoss > 0)
            {
                if (tackle.MpLoss >= Stats.MovementPoints.TotalInContext())
                {
                    return new List<CellRecord>();
                }

                OnTackled((short)tackle.MpLoss, (short)tackle.ApLoss, tacklers);
            }

            int index = 1;

            foreach (var cell in path.Skip(1))
            {
                tacklers = GetTacklers(cell);
                tackle = GetTackle(tacklers);

                if (tackle.Consistent())
                {
                    break;
                }

                index++;
            }

            path = path.Take(index + 1).ToList();

            return path.Take(1 + Stats.MovementPoints.TotalInContext()).ToList();
        }
        private IEnumerable<Fighter> GetTacklers(CellRecord cell)
        {
            return this.EnemyTeam.GetFighters<Fighter>().Where(x => x.IsMeleeWith(cell.Point) && x.CanTackle());
        }
        private Tackle GetTackle(IEnumerable<Fighter> tacklers)
        {
            double result = 1;

            foreach (var tackler in tacklers)
            {
                result *= (Stats[CharacteristicEnum.TACKLE_EVADE].TotalInContext() + 2) / (2d * (tackler.Stats[CharacteristicEnum.TACKLE_BLOCK].TotalInContext() + 2));
            }

            short looseAp = 0;
            short looseMp = 0;

            if (result < 1 && result > 0)
            {
                looseAp = (short)Math.Round(Stats.ActionPoints.TotalInContext() * (1 - result));
                looseMp = (short)Math.Round(Stats.MovementPoints.TotalInContext() * (1 - result));
            }

            return new Tackle(looseAp, looseMp);
        }
        public bool IsTackled()
        {
            return GetTackle().Consistent();
        }
        public Tackle GetTackle()
        {
            return GetTackle(GetTacklers(Cell));
        }

        public virtual void Move(List<CellRecord> path)
        {
            path.Insert(0, this.Cell);

            if (path.Count <= 1)
            {
                return;
            }

            if (Fight.Ended || !Fight.StartAcknowledged)
                return;


            if (!path.Skip(1).All(x => Fight.IsCellFree(x)))
            {
                this.OnMoveFailed(MovementFailedReason.Obstacle);
                return;
            }

            if (IsCarried())
            {
                using (Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_TRIGGERED))
                {
                    GetCarrier().Throw(path[1], true);
                    path.Remove(path[0]);

                }
            }

            for (int i = 1; i < path.Count; i++)
            {
                if (Fight.ShouldTriggerOnMove(path[i - 1].Id, path[i].Id))
                {
                    if (i + 1 <= path.Count)
                    {
                        path.RemoveRange(i + 1, path.Count - i - 1);
                    }
                    break;
                }
            }

            using (Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_MOVE))
            {
                short mpCost = (short)(path.Count - 1);

                if (mpCost <= Stats.MovementPoints.TotalInContext() && mpCost > 0 && Stats.MovementPoints.TotalInContext() > 0)
                {
                    path = ApplyTackle(path);

                    if (path.Count() > 0)
                    {
                        IsMoving = true;

                        mpCost = (short)(path.Count - 1);

                        this.Cell = Fight.Map.GetCell(path.Last().Id);

                        this.Direction = path[path.Count - 2].Point.OrientationTo(path[path.Count - 1].Point);

                        if (Stats.InvisibilityState == GameActionFightInvisibilityStateEnum.INVISIBLE)
                        {
                            Team.Send(new GameMapMovementMessage(path.Select(x => x.Id).ToArray(), -1, Id));
                        }
                        else
                        {
                            Fight.Send(new GameMapMovementMessage(path.Select(x => x.Id).ToArray(), -1, Id));
                        }

                        this.MovementHistory.OnMove(path);


                        this.LooseMp(this, mpCost, 0);

                        IsMoving = false;

                        TriggerMovementBuffs(new Movement(MovementType.Walk, this, mpCost));


                    }
                    else
                    {
                        this.OnMoveFailed(MovementFailedReason.Tackle);
                    }
                }
                else
                {
                    this.OnMoveFailed(MovementFailedReason.MissingMp);
                }

            }
        }

        public bool IsFriendlyWith(Fighter actor)
        {
            return actor.Team == this.Team;
        }
        public bool IsEnnemyWith(Fighter actor)
        {
            return !IsFriendlyWith(actor);
        }
        public void LooseMp(Fighter source, short amount, ActionsEnum action)
        {
            Stats.UseMp(amount);
            OnPointsVariation(source, action, (short)(-amount));
        }
        public void GainMp(Fighter source, short delta)
        {
            Stats.GainMp(delta);
            OnPointsVariation(source, ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_WIN, delta);
        }
        public void LooseAp(Fighter source, short amount, ActionsEnum action)
        {
            Stats.UseAp(amount);
            OnPointsVariation(source, action, (short)(-amount));
        }
        public void GainAp(Fighter source, short delta)
        {
            Stats.GainAp(delta);
            OnPointsVariation(source, ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_WIN, delta);
        }


        private void OnPointsVariation(Fighter source, ActionsEnum action, short delta)
        {
            Fight.Send(new GameActionFightPointsVariationMessage()
            {
                actionId = (short)action,
                delta = delta,
                sourceId = source.Id,
                targetId = Id,
            });


            switch (action)
            {
                case ActionsEnum.ACTION_CHARACTER_MOVEMENT_POINTS_LOST:
                    TriggerBuffs(TriggerTypeEnum.OnMPLost, null);

                    break;
                case ActionsEnum.ACTION_CHARACTER_ACTION_POINTS_LOST:
                    source.TriggerBuffs(TriggerTypeEnum.OnCasterRemoveAp, null);
                    TriggerBuffs(TriggerTypeEnum.OnAPLost, null);
                    break;

            }
        }
        public virtual void PassTurn()
        {
            if (!IsFighterTurn)
                return;

            Fight.StopTurn();

        }
        public void ModifyPlacement(short cellId, bool send = true)
        {
            if (!Fight.Started)
            {
                lock (Fight)
                {
                    this.Cell = Fight.Map.GetCell(cellId);

                    if (send)
                    {
                        this.Fight.UpdateFightersPlacementDirection();
                        this.Fight.UpdateEntitiesPositions();
                    }
                }
            }
        }
        /* public short GetSpellBoost<T>(short spellId) where T : SpellBoostBuff
         {
             var buffs = GetBuffs<T>().Where(x => x.SpellId == spellId);
             return (short)buffs.Sum(x => x.GetDelta());
         } */




        protected IEnumerable<SummonedFighter> GetSummons()
        {
            return Fight.GetFighters<SummonedFighter>(x => x.IsSummoned() && x.GetSummoner() == this);
        }

        public int GetSummonsCount()
        {
            return GetSummons().OfType<SummonedMonster>().Where(x => x.UseSummonSlot()).Count();
        }


        public abstract void OnTurnBegin();

        public void RemoveAndDispellBuff(Fighter source, Buff buff)
        {
            this.RemoveBuff(source, buff);
            buff.Dispell();
        }

        public void RemoveSpellEffects(Fighter source, short spellId)
        {
            IEnumerable<Buff> buffs = this.Buffs.Where(x => x.Cast.SpellId == spellId);

            foreach (var buff in buffs.ToArray())
            {
                RemoveAndDispellBuff(source, buff);

                if (buff.Cast.GetParent() != null)
                {
                    // DOFUS EBENE -> do not remove parent
                    //RemoveSpellEffects(source, buff.Cast.GetParent().SpellId);
                }
            }

            Fight.Send(new GameActionFightDispellSpellMessage(spellId, 0, source.Id,
                Id, true));
        }

        public bool HasBuff(Buff buff)
        {
            return Buffs.Contains(buff);
        }

        public void RemoveBuff(Fighter source, Buff buff)
        {
            this.Buffs.Remove(buff);
            Fight.Buffs.Remove(buff);

            Fight.Send(new GameActionFightDispellEffectMessage(buff.Id, 0, source.Id, buff.Target.Id, true));

            BuffIdProvider.Push(buff.Id);
        }
        [WIP("voir dofus nébuleux (Fight.Timeline.IndexOf(buff.Target) < Fight.Timeline.Index) ?")]
        public void AddBuff(Buff buff)
        {
            if (BuffMaxStackReached(buff)) // WIP censer cumuler la durée ?
            {
                Buff oldBuff = Buffs.FirstOrDefault(x => IsSimilar(x, buff));
                RemoveAndDispellBuff(this, oldBuff);
            }



            Fight.Buffs.Add(buff);
            Buffs.Add(buff);

            if (Trigger.IsInstant(buff.GetTriggers()) && !buff.HasDelay())
            {
                buff.Apply();
            }

            OnBuffAdded(buff);
        }
        public void OnBuffAdded(Buff buff)
        {
            //if (!buff.Silent)
            {
                var abstractFightDispellableEffect = buff.GetAbstractFightDispellableEffect();


                Fight.Send(new GameActionFightDispellableEffectMessage()
                {
                    actionId = buff.GetActionId(),
                    effect = abstractFightDispellableEffect,
                    sourceId = buff.Cast.Source.Id,
                });
            }

        }

        public void OnEffectDurationReduced(Fighter source, short actionId, short delta)
        {
            Fight.Send(new GameActionFightModifyEffectsDurationMessage()
            {
                actionId = actionId,
                delta = (short)(-delta),
                sourceId = source.Id,
                targetId = Id,
            });
        }

        public bool TriggerBuffs(TriggerTypeEnum type, ITriggerToken token, int? triggerParam = null)
        {
            bool result = false;

            IEnumerable<TriggerBuff> buffs = GetBuffs<TriggerBuff>().Where(
                x => x.Triggers.Any(x => x.Type == type && x.Value == triggerParam) && !x.HasDelay() && x.CanTrigger()).ToArray();

            foreach (var buff in buffs)
            {
                buff.LastTriggeredSequence = Fight.SequenceManager.CurrentSequence;

                if (buff.Apply(token))
                {
                    result = true;
                }
            }

            return result;
        }

        public IEnumerable<T> GetBuffs<T>() where T : Buff
        {
            return Buffs.OfType<T>();
        }
        public IEnumerable<Buff> GetBuffs()
        {
            return Buffs;
        }
        public bool BuffMaxStackReached(Buff buff)
        {
            bool result = buff.Cast.Spell.Level.MaxStack > 0 &&
                buff.Cast.Spell.Level.MaxStack <= this.Buffs.Count((Buff entry) => IsSimilar(entry, buff));

            return result;
        }
        /// <summary>
        /// && targetMasks (Fureur Iop) correct ?
        /// </summary>
        /// <param name="current"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        private bool IsSimilar(Buff current, Buff reference)
        {
            bool result = current.Cast.SpellId == reference.Cast.SpellId &&
                 current.Effect.EffectId == reference.Effect.EffectId && current.Effect.Delay == reference.Effect.Delay
                 && Trigger.SequenceEquals(current.GetTriggers(), reference.GetTriggers()) && current.GetType().Name == reference.GetType().Name
                 && current.Effect.TargetMask == reference.Effect.TargetMask;

            if (current is StateBuff && reference is StateBuff)
            {
                return result && ((StateBuff)current).Record.Id == ((StateBuff)reference).Record.Id;
            }

            return result;
        }
        public void SwapPlacementPosition(Fighter target)
        {
            short cellId = this.Cell.Id;

            this.ModifyPlacement(target.Cell.Id, false);
            target.ModifyPlacement(cellId, false);

            this.Fight.UpdateFightersPlacementDirection();
            this.Fight.UpdateEntitiesPositions();
        }
        public virtual bool CanPlay()
        {
            return Alive;
        }
        public virtual IFightResult GetFightResult()
        {
            return new FightResult(this, this.GetFighterOutcome(), Loot);
        }
        public virtual int GetDroppedKamas()
        {
            return 0;
        }
        public FightOutcomeEnum GetFighterOutcome()
        {
            bool flag = this.Team.Alives == 0;
            bool flag2 = this.EnemyTeam.Alives == 0;
            FightOutcomeEnum result;
            if (!flag && flag2)
            {
                result = FightOutcomeEnum.RESULT_VICTORY;
            }
            else
            {
                if (flag && !flag2)
                {
                    result = FightOutcomeEnum.RESULT_LOST;
                }
                else
                {
                    result = FightOutcomeEnum.RESULT_DRAW;
                }
            }
            return result;
        }
        public short GetMPDistance(Fighter other)
        {
            return Cell.Point.ManhattanDistanceTo(other.Cell.Point);
        }
        public virtual void OnJoined()
        {
            this.Fight.UpdateFightersPlacementDirection();
            this.ShowFighter();
            this.Fight.UpdateEntitiesPositions();
            Fight.UpdateTeams();

            FightEventApi.FighterJoined(this);
        }
        public void ShowFighter()
        {
            foreach (var characterFighter in Fight.GetAllConnectedFighters())
            {
                ShowFighter(characterFighter);
            }
        }

        public void RefreshStats(CharacteristicEnum characteristic)
        {
            foreach (CharacterFighter target in Fight.GetAllConnectedFighters())
            {
                target.Character.Client.Send(new RefreshCharacterStatsMessage(Id, Stats.GetGameFightCharacteristics(this, target, characteristic)));
            }
        }
        public void RefreshStats()
        {
            foreach (CharacterFighter target in Fight.GetAllConnectedFighters())
            {
                target.Character.Client.Send(new RefreshCharacterStatsMessage(Id, Stats.GetGameFightCharacteristics(this, target)));
            }
        }
        public void ShowFighter(CharacterFighter fighter)
        {
            fighter.Character.Client.Send(new GameFightShowFighterMessage(GetFightFighterInformations(fighter)));
        }

        public EntityDispositionInformations GetEntityDispositionInformations()
        {
            return new FightEntityDispositionInformations()
            {
                cellId = (short)Cell.Id,
                direction = (byte)Direction,
                carryingCharacterId = 0,
            };
        }

        /*
         * Before turn end.
         */
        public void OnTurnEnding()
        {
            using (Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_TURN_END))
            {
                if (Alive)
                {
                    Fight.TriggerMarks(this, MarkTriggerType.OnTurnEnd);
                    TriggerBuffs(TriggerTypeEnum.OnTurnEnd, null);
                    OnTurnEnded();

                    this.WasTeleportedInInvalidCell = false;
                }

            }
        }

        public abstract void OnTurnEnded();

        public IdentifiedEntityDispositionInformations GetIdentifiedEntityDispositionInformations()
        {
            return new IdentifiedEntityDispositionInformations()
            {
                cellId = Cell.Id,
                direction = (byte)Direction,
                id = Id,
            };
        }

        public bool IsInvisible()
        {
            return Stats.InvisibilityState == GameActionFightInvisibilityStateEnum.INVISIBLE;
        }

        public bool ExecuteSpell(short spellId, byte grade, CellRecord targetCell)
        {
            SpellRecord record = SpellRecord.GetSpellRecord(spellId);
            Spell spell = new Spell(record, record.GetLevel(grade));
            SpellCast cast = new SpellCast(this, spell, targetCell);
            cast.Force = true;
            return CastSpell(cast);
        }
        public bool CastSpell(int levelId)
        {
            SpellLevelRecord level = SpellLevelRecord.GetSpellLevel(levelId);

            if (level != null)
            {
                SpellRecord record = SpellRecord.GetSpellRecord(level.SpellId);
                Spell spell = new Spell(record, level);
                SpellCast cast = new SpellCast(this, spell, Cell);
                cast.Force = true;
                return this.CastSpell(cast);
            }

            return false;
        }
        public virtual bool CastSpell(short spellId, short cellId)
        {
            Spell spell = GetSpell(spellId);

            if (spell != null)
            {
                return this.CastSpell(new SpellCast(this, spell, Fight.Map.GetCell(cellId)));
            }
            else
            {
                return false;
            }
        }

        [WIP]
        public virtual bool CastSpell(SpellCast cast)
        {

            if (cast.Spell == null)
            {
                return false;
            }


            if (Fight.Ended)
                return false;

            if (!cast.Force && (!IsFighterTurn || !Alive))
                return false;

            var result = CanCastSpell(cast);

            if (result != SpellCastResult.OK && result != SpellCastResult.CELL_NOT_FREE && !cast.Force)
            {
                OnSpellCastFailed(cast);
                return false;
            }

            if (!FightEventApi.CanCastSpell(cast))
                return false;

            cast.Target = Fight.GetFighter(cast.TargetCell.Id); // sure about that ? (friction)


            using (Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL))
            {
                cast.Critical = RollCriticalDice(cast.Spell.Level);

                SpellCastHandler handler = SpellManager.Instance.CreateSpellCastHandler(cast);

                if (!handler.Initialize())
                {
                    OnSpellCastFailed(cast);
                    return false;
                }

                UpdateInvisibility(handler);

                if (handler.Cast.Animation)
                {
                    OnSpellCasting(handler);
                }

                if (!cast.ApFree)
                    LooseAp(this, GetApCost(cast.Spell.Level), 0);

                if (result != SpellCastResult.CELL_NOT_FREE)
                {
                    if (!handler.Execute())
                    {
                        Fight.Warn("Unable to cast spell : " + cast.Spell.Record.Name);
                    }
                }

                // this.RefreshStats();

                OnSpellCasted(handler);
            }



            Fight.CheckFightEnd();

            return true;
        }


        private bool CanBeReveals()
        {
            IEnumerable<Glyph> effectiveGlyphs = GetEffectiveGlyphs().OfType<GlyphAura>();

            foreach (var glyph in effectiveGlyphs)
            {
                bool res = GetBuffs<InvisibilityBuff>().Any(x => x.Cast.MarkSource == glyph);

                if (res)
                {
                    return true;
                }
            }

            return false;
        }
        public void Reveals()
        {
            if (!IsInvisible())
            {
                return;
            }

            foreach (var buff in GetBuffs<InvisibilityBuff>().ToArray())
            {
                RemoveAndDispellBuff(this, buff);
            }

        }

        protected virtual void OnSpellCasted(SpellCastHandler handler)
        {
            this.SpellHistory.RegisterCastedSpell(handler.Cast.Spell.Level, this.Fight.GetFighter(handler.Cast.TargetCell.Id));

            foreach (var summon in GetSummons())
            {
                if (handler.GetEffectHandlers().Contains(summon.GetSummoningEffect()))
                {
                    Fight.TriggerMarks(summon, MarkTriggerType.OnMove);
                }
            }
        }

        protected void UpdateInvisibility(SpellCastHandler castedSpellHandler)
        {
            if (IsInvisible() && !castedSpellHandler.Cast.Force)
            {
                if (castedSpellHandler.RevealsInvisible() && !CanBeReveals())
                {
                    Reveals();
                }
                else
                {
                    OnDetected();
                }

            }
        }
        [WIP("see stump")]
        private void OnSpellCasting(SpellCastHandler handler)
        {
            Fighter target = Fight.GetFighter(handler.Cast.TargetCell.Id);

            Fight.Send(new GameActionFightSpellCastMessage()
            {
                actionId = (short)ActionsEnum.ACTION_CAST_STARTING_SPELL,
                critical = (byte)handler.Cast.Critical,
                destinationCellId = handler.Cast.TargetCell.Id,
                portalsIds = new short[0],
                silentCast = handler.Cast.Silent,
                sourceId = this.Id,
                spellId = handler.Cast.Spell.Record.Id,
                spellLevel = handler.Cast.Spell.Level.Grade,
                targetId = target == null ? 0 : target.Id,
                verboseCast = handler.Cast.GetParent() == null, // not sure
            });


        }

        public virtual FightSpellCastCriticalEnum RollCriticalDice(SpellLevelRecord spell)
        {
            FightSpellCastCriticalEnum critical = FightSpellCastCriticalEnum.NORMAL;

            if (HasRandDownModifier())
            {
                return critical;
            }



            if (spell.CriticalHitProbability != 0 && this.Random.NextDouble() * 100 < spell.CriticalHitProbability + Stats[CharacteristicEnum.CRITICAL_HIT].TotalInContext())
                critical = FightSpellCastCriticalEnum.CRITICAL_HIT;

            return critical;
        }

        protected virtual void OnSpellCastFailed(SpellCast cast)
        {
            Fight.Send(new GameActionFightNoSpellCastMessage(cast.Spell.Record.Id == 0 ? 0 : (int)cast.Spell.Level.Id));
        }

        [WIP]
        public virtual SpellCastResult CanCastSpell(SpellCast cast)
        {

            if (cast.Force)
                return SpellCastResult.OK;

            if (!cast.IsConditionBypassed(SpellCastResult.CANNOT_PLAY) && (!IsFighterTurn || !Alive))
            {
                return SpellCastResult.CANNOT_PLAY;
            }

            if (!cast.IsConditionBypassed(SpellCastResult.HAS_NOT_SPELL) && !HasSpell(cast.Spell.Record.Id))
            {
                return SpellCastResult.HAS_NOT_SPELL;
            }

            var spellLevel = cast.Spell.Level;

            if (!cast.IsConditionBypassed(SpellCastResult.UNWALKABLE_CELL) && (!cast.TargetCell.Walkable || cast.TargetCell.NonWalkableDuringFight))
            {
                return SpellCastResult.UNWALKABLE_CELL;
            }

            if (!cast.IsConditionBypassed(SpellCastResult.NOT_ENOUGH_AP) && (this.Stats.ActionPoints.TotalInContext() < GetApCost(spellLevel)))
            {
                return SpellCastResult.NOT_ENOUGH_AP;
            }

            var cellfree = Fight.IsCellFree(cast.TargetCell);

            if (!cast.IsConditionBypassed(SpellCastResult.CELL_NOT_FREE) &&
                ((spellLevel.NeedFreeCell && !cellfree) || (spellLevel.NeedTakenCell && cellfree)))
            {
                return SpellCastResult.CELL_NOT_FREE;
            }

            if (!cast.IsConditionBypassed(SpellCastResult.STATE_FORBIDDEN) && spellLevel.StatesForbidden.Any(HasState))
            {
                return SpellCastResult.STATE_FORBIDDEN;
            }

            if (!cast.IsConditionBypassed(SpellCastResult.STATE_REQUIRED) &&
                spellLevel.StatesRequired.Any(state => !HasState(state)))
            {
                return SpellCastResult.STATE_REQUIRED;
            }

            if (!cast.IsConditionBypassed(SpellCastResult.NOT_IN_ZONE) &&
                !IsInCastZone(spellLevel, cast.CastCell.Point, cast.TargetCell.Point))
            {
                return SpellCastResult.NOT_IN_ZONE;
            }

            if (!cast.IsConditionBypassed(SpellCastResult.HISTORY_ERROR) &&
                   !SpellHistory.CanCastSpell(spellLevel, cast.TargetCell))
            {
                return SpellCastResult.HISTORY_ERROR;
            }

            if (!cast.IsConditionBypassed(SpellCastResult.NO_LOS) &&
                (cast.Spell.Level.CastTestLos && !Fight.CanBeSeen(cast.CastCell.Point, cast.TargetCell.Point))
                && !CanCastNoLOS(spellLevel.SpellId))
            {
                return SpellCastResult.NO_LOS;
            }

            return SpellCastResult.OK;
        }

        public bool CanCastNoLOS(short spellId)
        {
            return GetBuffs<SpellBoostRemoveLOSBuff>().Any(x => x.SpellId == spellId && x.GetDelta() == 1);
        }

        public bool IsInCastZone(SpellLevelRecord spellLevel, MapPoint castPoint, MapPoint cell)
        {
            Set set = GetSpellZone(spellLevel, castPoint);

            return set.BelongToSet(cell);
        }


        public Set GetSpellZone(SpellLevelRecord spellLevel, MapPoint point)
        {
            int range = GetSpellRange(spellLevel);

            int minimalRange = GetSpellMinimalRange(spellLevel);

            Set set;

            if (spellLevel.RangeCanBeBoosted)
            {
                range += Stats[CharacteristicEnum.RANGE].TotalInContext();

                if (range < minimalRange)
                    range = minimalRange;

                range = Math.Min(range, 63);
            }

            if (spellLevel.CastInDiagonal || spellLevel.CastInLine)
            {
                set = new CrossSet(point, (short)range, (short)minimalRange)
                {
                    AllDirections = spellLevel.CastInDiagonal && spellLevel.CastInLine,
                    Diagonal = spellLevel.CastInDiagonal
                };
            }
            else
            {
                set = new LozengeSet(point, range, minimalRange);

            }

            return set;
        }
        private int GetSpellMinimalRange(SpellLevelRecord level)
        {
            var range = (int)level.MinRange;
            range += SpellModifiers.GetModifier(level.SpellId, SpellModifierTypeEnum.RANGE_MIN);
            return range;
        }
        private int GetSpellRange(SpellLevelRecord level)
        {
            var range = (int)level.MaxRange;
            range += SpellModifiers.GetModifier(level.SpellId, SpellModifierTypeEnum.RANGE_MAX);
            return range;
        }
        public virtual bool HasSpell(short spellId)
        {
            return false;
        }

        public bool HasState(int stateId)
        {
            bool hasState = GetBuffs<StateBuff>().Any(x => x.Record.Id == stateId);
            bool isStateDisabled = GetBuffs<DisableStateBuff>().Any(x => x.StateId == stateId);
            return hasState && !isStateDisabled;
        }
        public void OnStateRemoved(StateBuff buff)
        {
            TriggerBuffs(TriggerTypeEnum.OnStateRemoved, buff, (short)buff.Record.Id);
        }

        public void OnStateAdded(StateBuff buff)
        {
            TriggerBuffs(TriggerTypeEnum.OnStateAdded, buff, (short)buff.Record.Id);
        }
        [WIP("remove this tuple")]
        public void TeleportToPortal(Fighter source)
        {
            Tuple<Portal, Portal> pair = PortalManager.Instance.GetPortalsTuple(Fight, this.Cell.Id);

            pair.Item1.Disable();
            pair.Item2.Disable();

            CellRecord cell = source.Fight.Map.GetCell(pair.Item2.CenterCell.Id);

            this.Teleport(source, cell);

            this.TriggerBuffs(TriggerTypeEnum.OnTeleportPortal, null);
        }
        public Telefrag Teleport(Fighter source, CellRecord targetCell, bool register = true)
        {
            if (targetCell == null)
            {
                return null;
            }
            if (!CanBeMoved())
            {
                return null;
            }
            Fighter otherTarget = Fight.GetFighter(targetCell.Id);

            if (otherTarget != null && otherTarget != this)
            {
                switch (source.Breed)
                {
                    case BreedEnum.Xelor:
                        this.SwitchPosition(otherTarget, register);
                        return new Telefrag(this, otherTarget);
                    case BreedEnum.Ouginak:
                        Fight.Warn("Ouginak teleport. Todo");
                        break;
                }
            }

            if (!targetCell.Walkable || targetCell.NonWalkableDuringFight || !targetCell.Point.IsInMap())
            {
                if (IsFighterTurn)
                {
                    this.WasTeleportedInInvalidCell = true;
                }
                return null;
            }
            if (!Fight.IsCellFree(targetCell))
            {
                return null;
            }

            var msg = new GameActionFightTeleportOnSameMapMessage()
            {
                actionId = (short)ActionsEnum.ACTION_CHARACTER_TELEPORT_ON_SAME_MAP,
                cellId = targetCell.Id,
                sourceId = source.Id,
                targetId = Id,
            };

            if (Stats.InvisibilityState == GameActionFightInvisibilityStateEnum.INVISIBLE)
            {
                Team.Send(msg);
            }
            else
            {
                Fight.Send(msg);
            }

            var oldCell = Cell;

            this.Cell = targetCell;

            if (register)
                MovementHistory.OnCellChanged(oldCell);

            TriggerMovementBuffs(new Movement(MovementType.Teleport, source, 0));

            return null;
        }

        public void PushBack(Fighter source, CellRecord castCell, short delta, CellRecord targetCell)
        {
            DirectionsEnum direction = 0;
            bool diagonal = false;

            if (targetCell.Id == Cell.Id)
            {
                if (castCell.Id == targetCell.Id)
                {
                    return;
                }
                diagonal = targetCell.Point.IsOnSameDiagonal(castCell.Point);
                direction = castCell.Point.OrientationTo(targetCell.Point, diagonal);
            }
            else
            {
                if (targetCell.Id == Cell.Id)
                {
                    return;
                }
                diagonal = Cell.Point.IsOnSameDiagonal(targetCell.Point);
                direction = targetCell.Point.OrientationTo(Cell.Point, diagonal);
            }
            if (diagonal) // Tir de barrage
            {
                delta /= 2;
            }

            Slide(source, direction, delta, MovementType.Push);
        }
        public void Advance(Fighter source, short delta, CellRecord targetCell)
        {
            bool diagonal = this.Cell.Point.IsOnSameDiagonal(targetCell.Point);
            DirectionsEnum direction = this.Cell.Point.OrientationTo(targetCell.Point, diagonal);
            source.Slide(source, direction, delta, MovementType.Pull);
        }
        public void Retreat(Fighter source, short delta, CellRecord targetCell)
        {
            bool diagonal = this.Cell.Point.IsOnSameDiagonal(targetCell.Point);
            DirectionsEnum direction = targetCell.Point.OrientationTo(this.Cell.Point, diagonal);
            source.Slide(source, direction, delta, MovementType.Push);
        }
        public void PullForward(Fighter source, CellRecord castCell, short delta, CellRecord targetCell)
        {
            DirectionsEnum direction = 0;

            if (targetCell.Id == Cell.Id)
            {
                if (targetCell.Id == castCell.Id)
                    return;

                bool diagonal = targetCell.Point.IsOnSameDiagonal(castCell.Point);
                direction = targetCell.Point.OrientationTo(castCell.Point, diagonal);
            }
            else
            {
                if (Cell.Id == targetCell.Id)
                    return;

                bool diagonal = Cell.Point.IsOnSameDiagonal(targetCell.Point);
                direction = Cell.Point.OrientationTo(targetCell.Point, diagonal);
            }

            this.Slide(source, direction, delta, MovementType.Pull);
        }
        private void InflictPushDamages(Fighter source, int n, bool headOn)
        {
            double num1 = headOn ? 4 : 8d;
            double num2 = ((source.Level / 2d) + (source.Stats[CharacteristicEnum.PUSH_DAMAGE_BONUS].TotalInContext()
                - this.Stats[CharacteristicEnum.PUSH_DAMAGE_REDUCTION].TotalInContext()) + 32d)
                 * (n / (double)num1);

            short delta = (short)num2;

            this.InflictDamage(new Damage(source, this, EffectSchoolEnum.Pushback, delta, delta, null));
        }
        public void UpdateLook(Fighter source)
        {
            ServerEntityLook finalLook = null;

            LookBuff lookBuff = GetBuffs<LookBuff>().LastOrDefault();

            double rescaleValue = GetBuffs<RescaleSkinBuff>().Sum(x => x.Delta);

            if (lookBuff != null)
            {
                finalLook = lookBuff.Look;
            }
            else
            {
                finalLook = BaseLook.Clone();
            }

            finalLook.Rescale(1 + rescaleValue);

            this.Look = finalLook;

            this.Fight.Send(new GameActionFightChangeLookMessage()
            {
                actionId = (short)ActionsEnum.ACTION_CHARACTER_CHANGE_LOOK,
                entityLook = Look.ToEntityLook(),
                sourceId = source.Id,
                targetId = Id,
            });
        }
        public virtual void TriggerMovementBuffs(Movement movement)
        {
            var source = movement.GetSource();

            if (movement.Type != MovementType.Walk && source != this)
            {
                this.LastAttacker = source;
            }
            if (movement.Type == MovementType.Walk && Carried != null)
            {
                Carried.Cell = this.Cell;
            }
            if (movement.Type != MovementType.Walk)
            {
                this.TriggerBuffs(TriggerTypeEnum.OnMoved, movement);
            }

            if (movement.Type == MovementType.Push)
            {
                this.TriggerBuffs(TriggerTypeEnum.OnPushed, movement);
            }

            if (movement.Type == MovementType.Pull)
            {
                this.TriggerBuffs(TriggerTypeEnum.OnPulled, movement);
            }

            if (movement.Type == MovementType.SwitchPosition)
            {
                this.TriggerBuffs(TriggerTypeEnum.OnSwitchPosition, movement);
            }

            if (movement.Type == MovementType.Walk)
            {
                for (int i = 0; i < movement.MpCost; i++)
                {
                    this.TriggerBuffs(TriggerTypeEnum.OnAMpUsed, movement);
                }

                this.TriggerBuffs(TriggerTypeEnum.OnMpUsed, movement);
            }

            Fight.TriggerMarks(this, MarkTriggerType.OnMove);

            Moved?.Invoke(this);

        }
        public void SetSpellCooldown(Fighter source, short spellId, short value)
        {
            SpellHistory.SetSpellCooldown(spellId, value);

            Fight.Send(new GameActionFightSpellCooldownVariationMessage()
            {
                actionId = (short)ActionsEnum.ACTION_CHARACTER_ADD_SPELL_COOLDOWN,
                sourceId = source.Id,
                spellId = spellId,
                targetId = Id,
                value = value,
            });

            OnSpellCooldownChanged(source, ActionsEnum.ACTION_CHARACTER_REMOVE_SPELL_COOLDOWN, spellId, value);
        }

        public abstract Spell GetSpell(short spellId);

        public abstract IEnumerable<SpellRecord> GetSpells();

        public void ReduceSpellCooldown(Fighter source, short spellId, short delta)
        {
            var newCooldown = SpellHistory.ReduceSpellCooldown(spellId, delta);
            OnSpellCooldownChanged(source, ActionsEnum.ACTION_CHARACTER_REMOVE_SPELL_COOLDOWN, spellId, (short)newCooldown);
        }
        private void OnSpellCooldownChanged(Fighter source, ActionsEnum action, short spellId, short delta)
        {
            Fight.Send(new GameActionFightSpellCooldownVariationMessage()
            {
                actionId = (short)action,
                sourceId = source.Id,
                spellId = spellId,
                targetId = Id,
                value = delta,
            });
        }
        public bool HasRandDownModifier()
        {
            return Buffs.OfType<RandModifierBuff>().Any(x => !x.Up);
        }
        public bool HasRandUpModifier()
        {
            return Buffs.OfType<RandModifierBuff>().Any(x => x.Up);
        }
        public short GetApCost(SpellLevelRecord level)
        {
            short apCost = level.ApCost;

            apCost -= this.SpellModifiers.GetModifier(level.SpellId, SpellModifierTypeEnum.AP_COST);

            if (apCost < 0)
            {
                apCost = 0;
            }
            return apCost;
        }
        public void Slide(Fighter source, DirectionsEnum direction, short delta, MovementType type)
        {
            if (!CanBeMoved())
            {
                return;
            }
            if (type == MovementType.Push && !CanBePushed())
            {
                return;
            }
            MapPoint destinationPoint = Cell.Point;

            for (int i = 0; i < delta; i++)
            {
                MapPoint oldPoint = destinationPoint;
                MapPoint targetPoint = destinationPoint.GetCellInDirection(direction, 1);

                if (targetPoint != null && Fight.IsCellFree(targetPoint.CellId))
                {
                    destinationPoint = targetPoint;

                    if (Fight.ShouldTriggerOnMove(oldPoint.CellId, targetPoint.CellId))
                    {
                        break;
                    }

                }
                else
                {
                    if (type == MovementType.Push)
                    {
                        InflictPushDamages(source, delta - i, true);
                        MapPoint next = destinationPoint.GetNearestCellInDirection(direction);

                        if (next != null)
                        {
                            Fighter nextFighter = Fight.GetFighter(next.CellId);

                            if (nextFighter != null)
                            {
                                nextFighter.InflictPushDamages(source, delta - i, false);
                            }
                        }
                    }
                    break;
                }
            }

            if (destinationPoint.CellId == Cell.Id)
            {
                return;
            }

            using (Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_MOVE))
            {
                var msg = new GameActionFightSlideMessage()
                {
                    actionId = 6,
                    endCellId = destinationPoint.CellId,
                    sourceId = source.Id,
                    targetId = Id,
                    startCellId = this.Cell.Id,
                };

                if (Stats.InvisibilityState == GameActionFightInvisibilityStateEnum.INVISIBLE)
                {
                    Team.Send(msg);
                }
                else
                {
                    Fight.Send(msg);
                }

            }

            var oldCell = this.Cell;

            this.Cell = Fight.Map.GetCell(destinationPoint);

            TriggerMovementBuffs(new Movement(type, source, 0));

            MovementHistory.OnCellChanged(oldCell);
        }
        [WIP("teleport triggered")]
        public void SwitchPosition(Fighter target, bool register = true)
        {
            if (!CanSwitchPosition() || !CanBeMoved() || !target.CanSwitchPosition() || !target.CanBeMoved())
                return;

            /*
             * 
             */
            if (LastExchangedPositionSequenced != null && target.LastExchangedPositionSequenced != null &&
                LastExchangedPositionSequenced == target.Cell && target.LastExchangedPositionSequenced == this.Cell)
            {
                return;
            }

            target.LastExchangedPositionSequenced = target.Cell;
            this.LastExchangedPositionSequenced = this.Cell;

            CellRecord cell = this.Cell;
            this.Cell = target.Cell;
            target.Cell = cell;


            Fight.Send(new GameActionFightExchangePositionsMessage()
            {
                actionId = 0,
                casterCellId = target.Cell.Id,
                sourceId = target.Id,
                targetCellId = this.Cell.Id,
                targetId = Id,
            });

            if (register)
            {
                target.MovementHistory.RegisterEntry(this.Cell);
                MovementHistory.RegisterEntry(cell);
            }
            else
            {
                target.MovementHistory.RegisterEntry(this.Cell);
            }

            TriggerMovementBuffs(new Movement(MovementType.SwitchPosition, this, 0));
            target.TriggerMovementBuffs(new Movement(MovementType.SwitchPosition, this, 0));

        }
        public void SetInvisiblityState(GameActionFightInvisibilityStateEnum state, Fighter source)
        {
            GameActionFightInvisibilityStateEnum oldState = this.Stats.InvisibilityState;
            this.Stats.InvisibilityState = state;
            OnInvisibilityStateChanged(state, oldState, source);
        }

        [WIP("show fighter, invalid")]
        private void OnInvisibilityStateChanged(GameActionFightInvisibilityStateEnum state, GameActionFightInvisibilityStateEnum oldState, Fighter source)
        {
            foreach (var fighter in Fight.GetAllConnectedFighters())
            {
                fighter.Send(new GameActionFightInvisibilityMessage()
                {
                    actionId = (short)ActionsEnum.ACTION_CHARACTER_MAKE_INVISIBLE,
                    sourceId = source.Id,
                    targetId = Id,
                    state = (byte)GetInvisibilityStateFor(fighter),
                });
            }

            if (oldState == GameActionFightInvisibilityStateEnum.INVISIBLE && state == GameActionFightInvisibilityStateEnum.VISIBLE)
            {
                ShowFighter();
            }
        }
        public GameActionFightInvisibilityStateEnum GetInvisibilityStateFor(Fighter fighter)
        {
            if (fighter.IsFriendlyWith(this) && this.Stats.InvisibilityState != GameActionFightInvisibilityStateEnum.VISIBLE)
            {
                return GameActionFightInvisibilityStateEnum.DETECTED;
            }
            else
            {
                return Stats.InvisibilityState;
            }
        }
        public bool BlockLOS()
        {
            return Stats.InvisibilityState != GameActionFightInvisibilityStateEnum.INVISIBLE;
        }
        private void OnDetected()
        {
            this.EnemyTeam.Send(new GameActionFightInvisibleDetectedMessage()
            {
                actionId = (short)ActionsEnum.ACTION_CHARACTER_MAKE_INVISIBLE,
                cellId = Cell.Id,
                sourceId = Id,
                targetId = Id,
            });
        }

        public abstract void Kick(Fighter source);

        public abstract GameFightFighterInformations GetFightFighterInformations(CharacterFighter to);

        public abstract FightTeamMemberInformations GetFightTeamMemberInformations();

        public virtual void OnFightStarted()
        {
            this.FightStartCell = this.Cell;
        }

        public short[] GetPreviousPositions()
        {
            return MovementHistory.GetEntries(2).Select(x => (short)x.Cell.Id).ToArray();
        }

        public virtual bool DisplayInTimeline()
        {
            return true;
        }

        public void DispelState(Fighter source, int stateId)
        {
            foreach (var buff in GetBuffs<StateBuff>().Where(x => x.Record.Id == stateId).ToArray())
            {
                RemoveAndDispellBuff(source, buff);
            }
        }
        public virtual bool CanUsePortal()
        {
            return true;
        }
        public virtual bool CanBeCarried()
        {
            return true;
        }
        public virtual bool CanTackle()
        {
            return !GetBuffs<StateBuff>().Where(x => x.Record.CantTackle).Any(y => HasState(y.StateId)) && !IsCarried() && !IsInvisible();
        }
        public virtual bool CanBeTackled()
        {
            return !GetBuffs<StateBuff>().Where(x => x.Record.CantBeTackled).Any(y => HasState(y.StateId));
        }
        public virtual bool CanDealDamages()
        {
            return !GetBuffs<StateBuff>().Where(x => x.Record.CantDealDamage).Any(y => HasState(y.StateId));
        }
        public virtual bool IsInvulnerableMelee()
        {
            return GetBuffs<StateBuff>().Where(x => x.Record.InvulnerableMelee).Any(y => HasState(y.StateId));
        }
        public virtual bool IsInvulnerableRange()
        {
            return GetBuffs<StateBuff>().Where(x => x.Record.InvulnerableRange).Any(y => HasState(y.StateId));
        }
        public virtual bool IsInvulnerable()
        {
            return GetBuffs<StateBuff>().Where(x => x.Record.Invulnerable).Any(y => HasState(y.StateId));
        }
        public virtual bool CanBeMoved()
        {
            return (!GetBuffs<StateBuff>().Where(x => x.Record.CantBeMoved).Any(y => HasState(y.StateId))) && !HasState(GravityState);
        }
        public virtual bool CanSwitchPosition()
        {
            return !GetBuffs<StateBuff>().Where(x => x.Record.CantSwitchPosition).Any(y => HasState(y.StateId));
        }
        public virtual bool CanBePushed()
        {
            return !GetBuffs<StateBuff>().Where(x => x.Record.CantBePushed).Any(y => HasState(y.StateId));
        }
        public virtual bool IsIncurable()
        {
            return GetBuffs<StateBuff>().Where(x => x.Record.Incurable).Any(y => HasState(y.StateId));
        }
        [WIP("trigger V ? ")]
        public void Heal(Healing healing)
        {
            if (healing.Delta <= 0 || IsIncurable())
            {
                return;
            }

            TriggerBuffs(TriggerTypeEnum.OnHealed, healing);

            if (healing.Delta <= 0 || IsIncurable())
            {
                return;
            }

            int delta = healing.Delta;

            if (Stats.LifePoints + healing.Delta > Stats.MaxLifePoints)
            {
                delta = Stats.MaxLifePoints - Stats.LifePoints;
            }


            if (delta > 0)
            {
                Stats.LifePoints += delta;

                Fight.Send(new GameActionFightLifePointsGainMessage()
                {
                    actionId = (short)ActionsEnum.ACTION_CHARACTER_LIFE_POINTS_WIN_WITHOUT_BOOST,
                    delta = delta,
                    sourceId = healing.Source.Id,
                    targetId = Id,
                });
            }

            TriggerBuffs(TriggerTypeEnum.LifeAffected, null); // not sure about this one!!
            TriggerBuffs(TriggerTypeEnum.LifePointsAffected, null);

        }

        private void DispellShieldBuffs(Fighter source, int amount)
        {
            short num = (short)amount;

            foreach (var buff in GetBuffs<ShieldBuff>().ToArray())
            {
                buff.Delta -= num;

                if (buff.Delta <= 0)
                {
                    num = (short)(-buff.Delta);
                    RemoveBuff(source, buff);
                }
                else
                {
                    UpdateBuff(this, buff);
                    break;
                }
            }
        }

        private void UpdateBuff(Fighter source, Buff buff)
        {
            Fight.Send(new GameActionFightDispellableEffectMessage()
            {
                actionId = (short)ActionsEnum.ACTION_ITEM_BUFF_CHANGE_DURATION,
                effect = buff.GetAbstractFightDispellableEffect(),
                sourceId = source.Id,
            });
        }
        [WIP("only spell damage reflection are mutlplied by wisdom")] // verify this information
        public virtual int CalculateDamageReflection(int damage)
        {
            var reflectDamages = Stats[CharacteristicEnum.REFLECT_DAMAGE].TotalInContext() * (1 + (Stats.Wisdom.TotalInContext() / 100));

            if (reflectDamages > damage / 2d)
                return (int)(damage / 2d);

            return reflectDamages;
        }
        public DamageResult InflictDamage(Damage damage)
        {
            damage.Compute();

            int delta = damage.Computed.Value;

            if (!Alive)
            {
                return DamageResult.Zero();
            }

            this.LastAttacker = damage.Source;

            if ((IsInvulnerable() || !damage.Source.CanDealDamages()) || (IsInvulnerableMelee() && damage.Source.IsMeleeWith(this))
             || (IsInvulnerableRange() && !damage.Source.IsMeleeWith(this)) || delta < 0)
            {
                TriggerBuffs(damage);
                return DamageResult.Zero();
            }

            TriggerBuffs(damage);

            delta = damage.Computed.Value;

            if (delta <= 0 || (!Alive))
            {
                return DamageResult.Zero();
            }

            int lifeLoss = 0;

            int shieldLoss = 0;

            int permanentDamages = CalculateErodedLife(delta);

            if (Stats.ShieldPoints > 0 && !damage.IgnoreShield)
            {
                if (Stats.ShieldPoints - delta <= 0)
                {
                    int num = delta - Stats.ShieldPoints; // effective life loose
                    permanentDamages = CalculateErodedLife(num);

                    Fight.Send(new GameActionFightLifeAndShieldPointsLostMessage()
                    {
                        actionId = 0,
                        elementId = (int)damage.EffectSchool,
                        loss = num,
                        shieldLoss = (short)Stats.ShieldPoints,
                        permanentDamages = permanentDamages,
                        sourceId = damage.Source.Id,
                        targetId = this.Id,
                    });

                    lifeLoss = num;
                    shieldLoss = Stats.ShieldPoints;

                    Stats.SetShield(0);

                    if (Stats.LifePoints - num <= 0)
                    {
                        lifeLoss = Stats.LifePoints;
                        Stats.LifePoints = 0;
                    }
                    else
                    {
                        Stats.MaxLifePoints -= permanentDamages;
                        Stats.LifePoints -= num;

                    }

                    DispellShieldBuffs(damage.Source, shieldLoss);
                }
                else
                {
                    Fight.Send(new GameActionFightLifeAndShieldPointsLostMessage()
                    {
                        actionId = 0,
                        elementId = (int)damage.EffectSchool,
                        loss = 0,
                        permanentDamages = 0,
                        shieldLoss = (short)delta,
                        sourceId = damage.Source.Id,
                        targetId = this.Id,
                    });

                    permanentDamages = 0;

                    shieldLoss = delta;

                    Stats.RemoveShield((short)delta);

                    DispellShieldBuffs(damage.Source, shieldLoss);
                }
            }
            else
            {
                if (Stats.LifePoints - delta <= 0)
                {
                    Fight.Send(new GameActionFightLifePointsLostMessage()
                    {
                        sourceId = damage.Source.Id,
                        targetId = this.Id,
                        actionId = 0,
                        elementId = (int)damage.EffectSchool,
                        loss = Stats.LifePoints,
                        permanentDamages = 0,
                    });

                    lifeLoss = (short)Stats.LifePoints;
                    Stats.LifePoints = 0;


                }
                else
                {

                    Stats.MaxLifePoints -= permanentDamages;
                    Stats.LifePoints -= delta;
                    lifeLoss = delta;


                    Fight.Send(new GameActionFightLifePointsLostMessage()
                    {
                        actionId = 0,
                        elementId = (int)damage.EffectSchool,
                        loss = delta,
                        permanentDamages = permanentDamages,
                        sourceId = damage.Source.Id,
                        targetId = this.Id,
                    });
                }
            }

            short reflected = (short)CalculateDamageReflection(damage.Computed.Value);

            if (reflected > 0)
            {
                damage.Source.InflictDamage(new Damage(this, damage.Source, EffectSchoolEnum.Fix,
                    reflected, reflected));
                OnDamageReflected(damage.Source);
            }

            DamageResult result = new DamageResult(lifeLoss, permanentDamages, shieldLoss);

            damage.OnApplied(result);
            TotalDamageReceivedSequenced += lifeLoss;

            if (this.Stats.LifePoints <= 0)
            {
                Die(damage.Source);
            }



            /*
             *  public function get_isLifeAffected() : Boolean
                 {
                    if(!(areLifePointsAffected || areMaxLifePointsAffected))
                    {          
                        return areErodedLifePointsAffected;
                     }
                    return true;
                 }
            */
            if (lifeLoss > 0)
            {
                TriggerBuffs(TriggerTypeEnum.LifeAffected, null);
                // dont know what we are doing? uff
                // check client HaveBuff.as (param1.areLifePointsAffected et param1.get_isLifeAffected())
                TriggerBuffs(TriggerTypeEnum.LifePointsAffected, null);

            }
            if (permanentDamages > 0)
            {
                TriggerBuffs(TriggerTypeEnum.ErodedLifePointsAffected, null);
                TriggerBuffs(TriggerTypeEnum.MaxLifePointsAffected, null);
            }



            return result;
        }

        public void OnDamageReflected(Fighter attacker)
        {
            this.Fight.Send(new GameActionFightReflectDamagesMessage()
            {
                actionId = (short)ActionsEnum.ACTION_CHARACTER_LIFE_LOST_REFLECTOR,
                sourceId = this.Id,
                targetId = attacker.Id,
            });
        }

        private void TriggerBuffs(Damage damage)
        {
            if (damage.EffectSchool == EffectSchoolEnum.Fix || damage.WontTriggerBuffs)
            {
                return;
            }

            if (damage.EffectHandler != null)
            {
                if (damage.EffectHandler.Effect.Duration > 0)
                {
                    // we dont trigger buffs for poisons?
                    return;
                }

                Mark? markSource = damage.EffectHandler.CastHandler.Cast.GetInitialMarkSource();

                if (markSource != null)
                {
                    switch (markSource.Type)
                    {
                        case GameActionMarkTypeEnum.GLYPH:
                            break;
                        case GameActionMarkTypeEnum.TRAP:
                            TriggerBuffs(TriggerTypeEnum.OnDamagedByTrap, damage);

                            if (!damage.Source.IsFriendlyWith(this))
                            {
                                TriggerBuffs(TriggerTypeEnum.OnDamagedByEnemyTrap, damage);
                            }
                            break;
                        case GameActionMarkTypeEnum.WALL:
                            break;
                        case GameActionMarkTypeEnum.PORTAL:
                            break;
                        case GameActionMarkTypeEnum.RUNE:
                            break;
                    }
                }
            }


            TriggerBuffs(TriggerTypeEnum.OnDamaged, damage);

            if (damage.Source.IsSummoned())
            {
                TriggerBuffs(TriggerTypeEnum.OnDamagedBySummon, damage);
            }


            switch (damage.EffectSchool)
            {
                case EffectSchoolEnum.Pushback:

                    if (damage.Source.IsFriendlyWith(this))
                    {
                        TriggerBuffs(TriggerTypeEnum.OnDamagedByAllyPush, damage);
                    }
                    else
                    {
                        TriggerBuffs(TriggerTypeEnum.OnDamagedByEnemyPush, damage);
                    }

                    TriggerBuffs(TriggerTypeEnum.OnDamagedByPush, damage);
                    break;
                case EffectSchoolEnum.Neutral:
                    damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageNeutral, damage);
                    TriggerBuffs(TriggerTypeEnum.OnDamagedNeutral, damage);
                    break;
                case EffectSchoolEnum.Earth:
                    damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageEarth, damage);
                    TriggerBuffs(TriggerTypeEnum.OnDamagedEarth, damage);
                    break;
                case EffectSchoolEnum.Water:
                    damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageWater, damage);
                    TriggerBuffs(TriggerTypeEnum.OnDamagedWater, damage);
                    break;
                case EffectSchoolEnum.Air:
                    damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageAir, damage);
                    TriggerBuffs(TriggerTypeEnum.OnDamagedAir, damage);
                    break;
                case EffectSchoolEnum.Fire:
                    damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageFire, damage);
                    TriggerBuffs(TriggerTypeEnum.OnDamagedFire, damage);
                    break;
            }
            if (damage.Source.IsMeleeWith(this))
            {
                damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageMelee, damage);
                TriggerBuffs(TriggerTypeEnum.OnDamagedMelee, damage);
            }
            else
            {
                damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageRange, damage);
                TriggerBuffs(TriggerTypeEnum.OnDamagedRange, damage);
            }

            if (damage.IsSpellDamage())
            {
                TriggerBuffs(TriggerTypeEnum.OnDamagedBySpell, damage);
            }
            else if (damage.IsWeaponDamage())
            {
                TriggerBuffs(TriggerTypeEnum.OnDamagedByWeapon, damage);
            }

            if (damage.Source.IsFriendlyWith(this))
            {

                TriggerBuffs(TriggerTypeEnum.OnDamagedByAlly, damage);
            }
            else
            {
                TriggerBuffs(TriggerTypeEnum.OnDamagedByEnemy, damage);
            }

            if (!damage.Source.IsFriendlyWith(this))
            {
                damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageEnnemy, damage);
            }
            else
            {
                damage.Source.TriggerBuffs(TriggerTypeEnum.CasterInflictDamageAlly, damage);
            }

            if (damage.EffectHandler != null && damage.EffectHandler.CastHandler.Cast.IsCriticalHit)
            {
                damage.Source.TriggerBuffs(TriggerTypeEnum.OnCriticalHit, damage);

                if (damage.Source.IsFriendlyWith(this))
                {
                    this.TriggerBuffs(TriggerTypeEnum.CasterCriticalHitOnAlly, damage);
                }
            }

        }
        public void OnStatsBuff(EffectsEnum effectEnum)
        {

            switch (effectEnum)
            {
                case EffectsEnum.Effect_SubRange:
                case EffectsEnum.Effect_SubRange_135:
                    TriggerBuffs(TriggerTypeEnum.OnRangeLost, null);
                    break;

                case EffectsEnum.Effect_SubAPPercent:
                case EffectsEnum.Effect_SubAP:
                case EffectsEnum.Effect_SubAP_Roll:
                    TriggerBuffs(TriggerTypeEnum.OnAPLost, null);
                    break;

                case EffectsEnum.Effect_SubMPPercent:
                case EffectsEnum.Effect_SubMP_Roll:
                case EffectsEnum.Effect_SubMP:
                    TriggerBuffs(TriggerTypeEnum.OnMPLost, null);
                    break;

            }
        }
        private int CalculateErodedLife(int damages)
        {
            var num = Stats.Erosion;

            if (num > 50)
            {
                num = 50;
            }
            return (int)(damages * (num / 100.0d));
        }
        public bool IsMeleeWith(Fighter fighter)
        {
            return this.Cell.Point.ManhattanDistanceTo(fighter.Cell.Point) <= 1;
        }
        public bool IsMeleeWith(MapPoint point)
        {
            return this.Cell.Point.ManhattanDistanceTo(point) == 1;
        }
        public IEnumerable<Fighter> GetMeleeFighters()
        {
            return Fight.GetFighters<Fighter>(x => x.IsMeleeWith(this));
        }
        public void KillAllSummons()
        {
            foreach (var summon in GetSummons().ToArray())
            {
                summon.Die(this);
            }
        }
        public void RemoveAndDispellAllBuffs(Fighter source, FightDispellableEnum dispellable = FightDispellableEnum.REALLY_NOT_DISPELLABLE)
        {
            foreach (var buff in Buffs.Where(x => x.Cast.Source == source && x.Dispellable <= dispellable).ToArray())
            {
                RemoveAndDispellBuff(source, buff);
            }
        }
        public void RemoveAllCastedBuffs()
        {
            foreach (Fighter current in this.Fight.GetFighters<Fighter>())
            {
                current.RemoveAndDispellAllBuffs(this);
            }
        }
        public IEnumerable<T> GetMarks<T>() where T : Mark
        {
            return GetMarks().OfType<T>();
        }
        public IEnumerable<Glyph> GetEffectiveGlyphs()
        {
            return Fight.GetMarks().OfType<Glyph>().Where(x => x.ContainsCell(Cell.Id));
        }
        public IEnumerable<Mark> GetMarks()
        {
            var marks = Fight.GetMarks().Where(x => x.Source == this);
            return marks;
        }
        public void RemoveMarks()
        {
            foreach (var mark in GetMarks().ToArray())
            {
                Fight.RemoveMark(mark);
            }
        }
        public virtual void OnDie(Fighter killedBy)
        {
            Killed?.Invoke(this, killedBy);

        }
        public bool IsCarrying()
        {
            return Carried != null;
        }
        public bool IsCarried()
        {
            return GetCarrier() != null;
        }
        public Fighter GetCarrier()
        {
            return Fight.GetFighters<Fighter>().FirstOrDefault(x => x.Carried == this);
        }
        public void Carry(Fighter target, SpellEffectHandler effectHandler)
        {
            effectHandler.AddStateBuff(this, SpellStateRecord.GetSpellStateRecord(CarrySpellState), FightDispellableEnum.REALLY_NOT_DISPELLABLE, -1);
            effectHandler.AddStateBuff(target, SpellStateRecord.GetSpellStateRecord(CarriedSpellState), FightDispellableEnum.REALLY_NOT_DISPELLABLE, -1);

            target.Cell = this.Cell;

            Carried = target;

            Fight.Send(new GameActionFightCarryCharacterMessage(target.Id,
                target.Cell.Id, (short)ActionsEnum.ACTION_CARRY_CHARACTER, Id));
        }
        public void Throw(CellRecord cell, bool drop)
        {
            if (IsCarrying())
            {
                Carried.Cell = cell;

                StateBuff buff = this.GetBuffs<StateBuff>().Where(x => x.StateId == CarrySpellState).FirstOrDefault();

                RemoveSpellEffects(this, buff.Cast.SpellId);
                Carried.RemoveSpellEffects(this, buff.Cast.SpellId);


                if (!drop)
                {
                    Fight.Send(new GameActionFightThrowCharacterMessage(Carried.Id, cell.Id, (short)ActionsEnum.ACTION_THROW_CARRIED_CHARACTER,
                        Id));
                }
                else
                {
                    Fight.Send(new GameActionFightDropCharacterMessage(Carried.Id, cell.Id, (short)ActionsEnum.ACTION_THROW_CARRIED_CHARACTER, Id));
                }

                Carried = null;
            }
        }
        public bool CanSummon()
        {
            return GetSummonsCount() < Stats[CharacteristicEnum.MAX_SUMMONED_CREATURES_BOOST].TotalInContext();
        }
        public void Die(Fighter killedBy)
        {
            if (Alive)
            {
                using (var sequence = Fight.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_CHARACTER_DEATH))
                {
                    this.Stats.LifePoints = 0;
                    TriggerBuffs(TriggerTypeEnum.OnDeath, new Death(killedBy));

                    KillAllSummons();
                    RemoveAllCastedBuffs();
                    this.RemoveMarks();

                    this.DeathTime = DateTime.Now;
                    Fight.Send(new GameActionFightDeathMessage()
                    {
                        actionId = (short)ActionsEnum.ACTION_CHARACTER_KILL,
                        sourceId = killedBy.Id,
                        targetId = this.Id,
                    });

                    this.Alive = false;


                    this.OnDie(killedBy);
                }

                if (IsFighterTurn)
                {
                    PassTurn();
                }

            }
            else
            {
                Fight.Warn("Cannot kill " + this + ", he is already dead!");
            }

        }
        public void OnDodge(Fighter source, ActionsEnum action, int delta)
        {
            Fight.Send(new GameActionFightDodgePointLossMessage()
            {
                actionId = (short)action,
                amount = (short)delta,
                sourceId = source.Id,
                targetId = Id,
            });
        }
        public virtual bool RollMPLose(Fighter from, short value)
        {
            var mpAttack = from.Stats[CharacteristicEnum.MP_REDUCTION].TotalInContext() > 1 ? from.Stats[CharacteristicEnum.MP_REDUCTION].TotalInContext() : 1;
            var mpDodge = Stats[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY].TotalInContext() > 1 ? Stats[CharacteristicEnum.DODGE_MP_LOST_PROBABILITY].TotalInContext() : 1;
            var prob = ((Stats.MovementPoints.TotalInContext() - value) / (double)(Stats.MovementPoints.TotalInContext())) * (mpAttack / (double)mpDodge) / 2d;

            if (prob < 0.10)
                prob = 0.10;
            else if (prob > 0.90)
                prob = 0.90 - (0.10 * value);

            var rnd = this.Random.NextDouble();


            return rnd < prob;
        }
        public virtual bool RollAPLose(Fighter from, int value)
        {
            var apAttack = from.Stats[CharacteristicEnum.AP_REDUCTION].TotalInContext() > 1 ? from.Stats[CharacteristicEnum.AP_REDUCTION].TotalInContext() : 1;
            var apDodge = Stats[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY].TotalInContext() > 1 ? Stats[CharacteristicEnum.DODGE_AP_LOST_PROBABILITY].TotalInContext() : 1;
            var prob = ((Stats.ActionPoints.TotalInContext() - value) / (double)(Stats.ActionPoints.TotalInContext())) * (apAttack / (double)apDodge) / 2d;

            if (prob < 0.10)
                prob = 0.10;
            else if (prob > 0.90)
                prob = 0.90;

            var rnd = this.Random.NextDouble();

            return rnd < prob;
        }
        public int CalculateArmorValue(short reduction)
        {
            return (int)(reduction * (100 + 5 * Level) / 100d);
        }

        public void OnDamageReduced(Damage damage, int dmgReduction)
        {
            Fight.Send(new GameActionFightReduceDamagesMessage()
            {
                actionId = 105,
                amount = dmgReduction,
                sourceId = damage.Source.Id,
                targetId = damage.Target.Id,
            });
        }
        public virtual bool IsSummoned()
        {
            return false;
        }
        public virtual SpellEffectHandler GetSummoningEffect()
        {
            return null;
        }
        public virtual Fighter GetSummoner()
        {
            return null;
        }
        public virtual Fighter GetController()
        {
            return null;
        }
        public virtual bool MustSkipTurn()
        {

            return (!Alive) || Buffs.OfType<SkipTurnBuff>().Any();
        }
        public override string ToString()
        {
            return $"{Id} : {Name}";
        }


        public bool HasDamageSharingBuff(DamageSharing effect)
        {
            return GetBuffs<TriggerBuff>().Any(x => x.Effect.EffectEnum == EffectsEnum.Effect_DamageSharing
            && x.Cast == effect.CastHandler.Cast);
        }




    }

}