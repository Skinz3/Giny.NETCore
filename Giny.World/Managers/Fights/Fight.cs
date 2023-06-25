using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Network.Messages;
using Giny.Core.Pool;
using Giny.Core.Time;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.Protocol.Types;
using Giny.World.Api;
using Giny.World.Managers.Actions;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Managers.Fights.Buffs;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Cast.Units;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Managers.Fights.Marks;
using Giny.World.Managers.Fights.Results;
using Giny.World.Managers.Fights.Sequences;
using Giny.World.Managers.Fights.Synchronisation;
using Giny.World.Managers.Fights.Timeline;
using Giny.World.Managers.Fights.Triggers;
using Giny.World.Managers.Fights.Zones;
using Giny.World.Managers.Idols;
using Giny.World.Managers.Maps;
using Giny.World.Network;
using Giny.World.Records.Idols;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rune = Giny.World.Managers.Fights.Marks.Rune;

namespace Giny.World.Managers.Fights
{
    public abstract class Fight : INetworkEntity
    {
        public const int TurnTime = 30;

        public const int SynchronizerTimout = 15;

        public const int TurnBeforeDisconnection = 20;

        private ReversedUniqueIdProvider m_contextualIdPopper = new ReversedUniqueIdProvider(0);

        private UniqueIdProvider m_markIdPopper = new UniqueIdProvider(0);

        public event Action<Fighter> TurnEnded;

        public int Id
        {
            get;
            private set;
        }
        public long? TargetMapId
        {
            get;
            set;
        }
        public FightTeam BlueTeam
        {
            get;
            private set;
        }
        public abstract FightTypeEnum FightType
        {
            get;
        }

        protected Character Origin
        {
            get;
            private set;
        }

        public FightTeam RedTeam
        {
            get;
            private set;
        }
        public MapRecord Map
        {
            get;
            private set;
        }
        /*
         * Fight will begin shortly.
         * (We casted initial spells, waiting 
         * for client ack.)
         */
        public bool Started
        {
            get;
            private set;
        }
        /*
         * Fight Started and 
         * server acknoledged it.
         */
        public bool StartAcknowledged
        {
            get;
            private set;
        }
        public CellRecord Cell
        {
            get;
            private set;
        }
        public DateTime CreationTime
        {
            get;
            private set;
        }
        public Synchronizer Synchronizer
        {
            get;
            private set;
        }

        public DateTime? StartTime
        {
            get;
            private set;
        }
        public abstract bool ShowBlades
        {
            get;
        }
        public abstract bool SpawnJoin
        {
            get;
        }

        public Fighter FighterPlaying
        {
            get
            {
                return this.Timeline.Current;
            }
        }

        protected ActionTimer PlacementTimer
        {
            get;
            set;
        }

        public FightTimeline Timeline
        {
            get;
            private set;
        }
        public int RoundNumber
        {
            get
            {
                return Timeline.RoundNumber;
            }
        }
        public bool Ended
        {
            get;
            private set;
        }

        public SequenceManager SequenceManager
        {
            get;
            private set;
        }
        public FightTeam Winners
        {
            get;
            private set;
        }

        private ActionTimer m_turnTimer;

        private List<Mark> Marks
        {
            get;
            set;
        }
        public List<Buff> Buffs
        {
            get;
            private set;
        }
        public DateTime TurnStartTime
        {
            get;
            private set;
        }

        protected IdolsInventory Idols
        {
            get;
            private set;
        }
        #region Events

        public event Action<Fight, Fighter> TurnStarted;

        #endregion

        public void Send(NetworkMessage message)
        {
            BlueTeam.Send(message);
            RedTeam.Send(message);
        }
        public void OnFighters<T>(Action<T> action, bool aliveOnly = true) where T : Fighter
        {
            BlueTeam.OnFighters(action, aliveOnly);
            RedTeam.OnFighters(action, aliveOnly);
        }

        public abstract void OnFighterJoined(Fighter fighter);

        public Fighter GetFighter(short cellId)
        {
            Fighter target = BlueTeam.GetFighter<Fighter>(x => x.Cell.Id == cellId);
            return target == null ? RedTeam.GetFighter<Fighter>(x => x.Cell.Id == cellId) : target;
        }
        public T GetFighter<T>(Func<T, bool> predicate) where T : Fighter
        {
            T result = BlueTeam.GetFighter(predicate);

            if (result == null)
            {
                result = RedTeam.GetFighter(predicate);
            }
            return result;
        }
        public List<Fighter> GetFighters(bool aliveOnly = true)
        {
            List<Fighter> fighters = new List<Fighter>();
            fighters.AddRange(RedTeam.GetFighters<Fighter>(aliveOnly));
            fighters.AddRange(BlueTeam.GetFighters<Fighter>(aliveOnly));
            return fighters;
        }
        public IEnumerable<Fighter> GetFighters(IEnumerable<CellRecord> cells)
        {
            return GetFighters().Where(x => cells.Contains(x.Cell));
        }
        public IEnumerable<T> GetFighters<T>(bool aliveOnly = true)
        {
            return GetFighters(aliveOnly).OfType<T>();
        }
        public IEnumerable<CharacterFighter> GetAllConnectedFighters()
        {
            return GetFighters<CharacterFighter>(false).Where(x => !x.Disconnected);
        }
        public IEnumerable<T> GetFighters<T>(Func<T, bool> predicate, bool aliveOnly = true)
        {
            return GetFighters<T>(aliveOnly).Where(predicate);
        }
        public int GetTurnIndex()
        {

            return Timeline.Index;
        }

        public Fight(Character origin, int id, MapRecord map, FightTeam blueTeam, FightTeam redTeam, CellRecord cell)
        {
            this.Id = id;
            this.Map = map;
            this.Origin = origin;
            this.BlueTeam = blueTeam;
            this.RedTeam = redTeam;
            this.BlueTeam.Fight = this;
            this.RedTeam.Fight = this;
            this.Timeline = new FightTimeline(this);
            this.Cell = cell;
            this.Started = false;
            this.StartAcknowledged = false;
            this.CreationTime = DateTime.Now;
            this.SequenceManager = new SequenceManager(this);
            this.Synchronizer = null;
            this.Marks = new List<Mark>();
            this.Buffs = new List<Buff>();
        }

        public void OnSequenceStarted(FightSequence sequence)
        {

        }
        public void OnSequenceEnded(FightSequence sequence)
        {
            if (sequence.Type == SequenceTypeEnum.SEQUENCE_SPELL && sequence.Parent == null)
            {
                foreach (var fighter in GetFighters<Fighter>())
                {
                    fighter.LastExchangedPositionSequenced = null;
                    fighter.TotalDamageReceivedSequenced = 0;
                }
            }
        }
        public virtual void OnSetReady(Fighter fighter, bool isReady)
        {
            this.Send(new GameFightHumanReadyStateMessage(fighter.Id, isReady));
            this.CheckFightStart();

        }
        public void UpdateTeams()
        {
            if (!Started)
            {
                BlueTeam.Update();
                RedTeam.Update();
            }
        }
        public FightTeam[] GetTeams()
        {
            return new FightTeam[] { BlueTeam, RedTeam };
        }
        public FightTeam GetTeam(TeamTypeEnum teamType)
        {
            if (BlueTeam.Type == teamType)
                return BlueTeam;
            if (RedTeam.Type == teamType)
                return RedTeam;

            return null;
        }
        public void UpdateFightersPlacementDirection()
        {
            OnFighters<Fighter>(x => x.FindPlacementDirection());
        }
        public void ShowFighters(CharacterFighter fighter)
        {
            OnFighters<Fighter>(target => target.ShowFighter(fighter));
        }
        public short GetPlacementTimeLeft()
        {
            if (Started)
            {
                return 0;
            }
            double num = GetPlacementDelay() - (DateTime.Now - this.CreationTime).TotalSeconds;
            if (num < 0.0)
            {
                num = 0.0;
            }
            return (short)(num * 10d);
        }
        public void UpdateEntitiesPositions()
        {
            List<IdentifiedEntityDispositionInformations> positions = new List<IdentifiedEntityDispositionInformations>();

            foreach (var fighter in GetFighters())
            {
                positions.Add(fighter.GetIdentifiedEntityDispositionInformations());
            }
            this.Send(new GameEntitiesDispositionMessage(positions.ToArray()));
        }
        public virtual int GetPlacementDelay()
        {
            return 0;
        }

        public void StartPlacement()
        {
            if (GetPlacementDelay() > 0)
            {
                this.PlacementTimer = new ActionTimer(GetPlacementDelay() * 1000, StartFighting, false);
                this.PlacementTimer.Start();
            }

            if (ShowBlades)
            {
                ShowBladesOnMap();
            }

            OnPlacementStarted();

            FightEventApi.PlacementStarted(this);
            Origin.OnInitiateFight(this);

        }

        protected virtual void OnPlacementStarted()
        {
            if (Origin.HasParty)
            {
                Idols = Origin.Party.IdolsInventory;
            }
            else
            {
                Idols = Origin.IdolsInventory;

            }
            this.Send(new IdolFightPreparationUpdateMessage(0, Idols.GetActiveIdols().Select(x => x.GetIdol()).ToArray()));
        }

        private void ShowBladesOnMap()
        {
            this.FindBladesPlacement();
            this.Map.Instance.AddFight(this);
        }
        private void FindBladesPlacement()
        {
            if (this.RedTeam.Leader.RoleplayCell.Id != this.BlueTeam.Leader.RoleplayCell.Id)
            {
                this.RedTeam.BladesCell = MapsManager.Instance.SecureRoleplayCell(this.Map, this.RedTeam.Leader.RoleplayCell);
                this.BlueTeam.BladesCell = MapsManager.Instance.SecureRoleplayCell(this.Map, this.BlueTeam.Leader.RoleplayCell);

            }
            else
            {
                this.BlueTeam.BladesCell = this.BlueTeam.Leader.RoleplayCell;

                CellRecord target = MapsManager.Instance.GetNearFreeCell(Map, this.BlueTeam.Leader.RoleplayCell);

                if (target == null)
                {
                    this.RedTeam.BladesCell = this.Map.RandomWalkableCell();
                }
                else
                {
                    this.RedTeam.BladesCell = Map.GetCell(target.Id);
                }

            }
        }

        public bool IsCellFree(CellRecord cell)
        {
            return cell.Walkable && !cell.NonWalkableDuringFight && GetFighter(cell.Id) == null;
        }
        public bool IsCellFree(short cellId)
        {
            return IsCellFree(Map.Cells[cellId]);
        }
        public int PopNextContextualId()
        {
            return m_contextualIdPopper.Pop();
        }
        public int PopNextMarkId()
        {
            return m_markIdPopper.Pop();
        }
        public virtual void StartFighting()
        {
            if (GetPlacementDelay() > 0)
            {
                this.PlacementTimer.Dispose();
                this.PlacementTimer = null;
            }

            this.StartTime = DateTime.Now;

            this.Started = true;

            UpdateEntitiesPositions();

            this.Map.Instance.RemoveBlades(this);

            this.Timeline.OrderLine();

            this.Send(new GameFightStartMessage(GetIdols()));

            this.UpdateTimeLine();

            this.Synchronize();

            this.UpdateRound();

            foreach (var fighter in GetFighters())
            {
                fighter.OnFightStarted();
            }

            this.CastIdols();

            this.OnFightStarted();

            Synchronizer = Synchronizer.RequestCheck(SynchronizerRole.StartFight, this, StartFight, LagAndStartFight, SynchronizerTimout * 1000);

        }

        public Idol[] GetIdols()
        {
            return Idols.GetActiveIdols().Select(x => x.GetIdol()).ToArray();
        }

        private void CastIdols()
        {
            var targetTeam = this.GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER);

            if (targetTeam == null)
            {
                return;
            }

            var targetFighters = targetTeam.GetFighters<MonsterFighter>();

            foreach (var idol in Idols.GetActiveIdols())
            {
                if (targetFighters.Any(x => idol.IncompatibleMonsters.Contains((int)x.Record.Id)))
                {
                    continue;
                }

                foreach (var enemy in targetFighters)
                {
                    SpellCast cast = new SpellCast(enemy, idol.Spell, enemy.Cell);
                    cast.Target = enemy;
                    cast.Force = true;
                    enemy.CastSpell(cast);
                }

            }
        }

        public abstract void OnFightStarted();

        private void StartFight()
        {
            this.StartAcknowledged = true;
            Synchronizer = null;
            StartTurn();
        }
        private void LagAndStartFight(CharacterFighter[] laggers)
        {
            if (Synchronizer == null)
                return;

            OnLaggersSpotted(laggers);

            StartFight();
        }
        private void StartTurn()
        {
            if (StartAcknowledged && !Ended && !CheckFightEnd())
            {
                this.OnTurnStarted();
            }
        }

        private void OnTurnStarted()
        {
            SequenceManager.ResetSequences();

            if (Timeline.NewRound)
            {
                UpdateRound();
            }

            Synchronize();

            this.Send(new GameFightTurnStartMessage(this.FighterPlaying.Id, Fight.TurnTime * 10));

            using (SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_TURN_START))
            {
                FighterPlaying.TurnStartCell = FighterPlaying.Cell;

                /*
                 * Here or after decrement buff delay ? seems here, see Dofus Ocre
                 */

                if (RoundNumber > 1)
                {
                    foreach (var buff in Buffs.ToArray())
                    {
                        if (!Timeline.IsIndexValid(buff.TurnIndex))
                        {
                            for (int i = buff.TurnIndex; i >= 0; i--)
                            {
                                if (Timeline.IsIndexValid(i))
                                {
                                    buff.TurnIndex = i;
                                    break;
                                }
                            }
                        }
                    }


                    this.DecrementBuffsDuration();
                    this.DecrementBuffsDelay();
                }
                this.DecrementMarkDurations(FighterPlaying);
                this.TriggerMarks(FighterPlaying, MarkTriggerType.OnTurnBegin);
                FighterPlaying.TriggerBuffs(TriggerTypeEnum.OnTurnBegin, null);

                FighterPlaying.TriggerBuffs(TriggerTypeEnum.AfterTurnBegin, null);

            }

            /*
            * If buffs killed fighter (iop vitality for instance)
            */
            if (FighterPlaying.Stats.LifePoints <= 0)
            {
                FighterPlaying.Die(FighterPlaying);
            }



            if (CheckFightEnd())
            {
                return;
            }

            if (FighterPlaying.MustSkipTurn())
            {
                StopTurn();
                return;
            }


            TurnStartTime = DateTime.Now;

            this.m_turnTimer = new ActionTimer((int)Fight.TurnTime * 1000, StopTurn, false);
            this.m_turnTimer.Start();

            FighterPlaying.OnTurnBegin();

            TurnStarted?.Invoke(this, FighterPlaying);
        }

        private void DecrementBuffsDelay()
        {
            foreach (var buff in Buffs.OfType<TriggerBuff>().Where(x => x.HasDelay() && x.TurnIndex == GetTurnIndex()).ToArray())
            {
                if (buff.DecrementDelay())
                {
                    buff.Apply();
                    buff.Target.RemoveAndDispellBuff(buff.GetSource(), buff);
                }
            }
        }
        private void DecrementBuffsDuration()
        {
            foreach (var buff in Buffs.ToArray().Where(x => x.TurnIndex == GetTurnIndex()))
            {
                if (!buff.HasDelay())
                {
                    if (buff.DecrementDuration() && buff.Target.HasBuff(buff))
                    {
                        buff.Target.RemoveAndDispellBuff(buff.GetSource(), buff);
                    }
                }
            }
        }



        public void StopTurn()
        {
            if (Synchronizer != null && Synchronizer.Role == SynchronizerRole.EndTurn)
            {
                /*
                 * Pass Turn multiple times 
                 * Chill out ! :D
                 */
                return;
            }

            if (Ended)
                return;

            if (m_turnTimer != null)
                m_turnTimer.Dispose();

            if (Synchronizer != null)
            {
                this.Reply("Last ReadyChecker was not disposed. (" + Synchronizer.Role + ")", Color.Red);
                Synchronizer.Cancel();
                Synchronizer = null;
            }

            OnTurnStopped();

            if (CheckFightEnd())
                return;

            Synchronizer = Synchronizer.RequestCheck(SynchronizerRole.EndTurn, this, PassTurnAndCheck, LagAndPassTurn, SynchronizerTimout * 1000);

        }
        protected void PassTurnAndCheck()
        {
            if (Synchronizer == null)
                return;

            Synchronizer = null;

            TurnEnded?.Invoke(FighterPlaying);

            FighterPlaying.Stats.ResetUsedPoints();
            PassTurn();
        }
        public TimeSpan GetTurnTimeLeft()
        {
            if (Timeline.Current == null)
                return TimeSpan.Zero;

            var time = (DateTime.Now - TurnStartTime).TotalMilliseconds;

            return TimeSpan.FromMilliseconds(time > 0 ? ((TurnTime * 1000) - (int)time) : 0);
        }
        public void Warn(string message)
        {
            Reply(message, Color.Orange);
        }
        public void Reply(string message, Color color)
        {
            foreach (var fighter in GetAllConnectedFighters())
            {
                fighter.Character.Reply(message, color);
            }
        }
        public void TextInformation(TextInformationTypeEnum type, short messageId, params object[] parameters)
        {
            foreach (var fighter in GetAllConnectedFighters())
            {
                fighter.Character.TextInformation(type, messageId, parameters);
            }
        }

        protected void PassTurn()
        {
            if (Ended)
                return;

            if (CheckFightEnd())
                return;

            if (!Timeline.SelectNextFighter())
            {
                if (!CheckFightEnd())
                {
                    this.Reply("Something goes wrong : no more actors are available to play but the fight is not ended", Color.Red);
                }

                return;
            }

            OnTurnPassed();

            StartTurn();
        }

        public void TriggerMarks(Fighter target, MarkTriggerType triggerType)
        {
            Mark[] marks = Marks.Where(x => x.Triggers.HasFlag(triggerType) && x.ContainsCell(target.Cell.Id)).ToArray();

            foreach (var mark in marks)
            {
                using (this.SequenceManager.StartSequence(SequenceTypeEnum.SEQUENCE_GLYPH_TRAP))
                {
                    mark.Trigger(target, triggerType);
                }
            }
        }
        public IEnumerable<T> GetMarks<T>() where T : Mark
        {
            return GetMarks().OfType<T>();
        }
        public IEnumerable<Mark> GetMarks()
        {
            return this.Marks;
        }
        public bool MarkExist<T>(Func<T, bool> markExist) where T : Mark
        {
            return Marks.OfType<T>().Any(markExist);
        }
        public bool ShouldTriggerOnMove(short oldCell, short cellId)
        {
            bool flag1 = Marks.OfType<Glyph>().Any(x => x.StopMovement &&
            (!x.ContainsCell(oldCell) && x.ContainsCell(cellId) || x.ContainsCell(oldCell) && !x.ContainsCell(cellId)));
            bool flag2 = Marks.OfType<Trap>().Any(x => x.StopMovement && x.ContainsCell(cellId));

            return flag1 || flag2;
        }
        private void OnTurnPassed()
        {
            // end sequence ?
        }
        private void LagAndPassTurn(CharacterFighter[] laggers)
        {
            if (Synchronizer == null)
                return;

            // some guys are lagging !
            OnLaggersSpotted(laggers);

            PassTurnAndCheck();
        }
        private void OnLaggersSpotted(CharacterFighter[] laggers)
        {
            if (laggers.Length == 1)
            {
                OnFighters<CharacterFighter>(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 28, laggers[0].Name));
            }
            else if (laggers.Length > 1)
            {
                OnFighters<CharacterFighter>(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 29, string.Join(",", laggers.Select(entry => entry.Name))));

            }
        }
        private void OnTurnStopped()
        {
            FighterPlaying.OnTurnEnding();

            if (SequenceManager.IsSequencing)
                SequenceManager.EndAllSequences();


            Send(new GameFightTurnEndMessage(FighterPlaying.Id));
        }

        public GameActionMark GetGameActionMark(CharacterFighter fighter, Mark mark)
        {
            GameActionMark gameActionMark = null;

            if (mark.IsVisibleFor(fighter))
            {
                gameActionMark = mark.GetGameActionMark();
            }
            else
            {
                gameActionMark = mark.GetHiddenGameActionMark();
            }
            return gameActionMark;
        }
        public void AddMark(Mark mark)
        {
            if (!mark.CenterCell.Walkable)
            {
                return;
            }
            this.Marks.Add(mark);

            foreach (var fighter in GetAllConnectedFighters())
            {
                var gameActionMark = GetGameActionMark(fighter, mark);
                fighter.Send(new GameActionFightMarkCellsMessage(gameActionMark, 0, mark.Source.Id));
            }

            mark.OnAdded();
        }
        public void RemoveMark(Mark mark)
        {
            this.Marks.Remove(mark);

            this.Send(new GameActionFightUnmarkCellsMessage((short)mark.Id, 0, mark.Source.Id));

            mark.OnRemoved();
        }
        private void DecrementMarkDurations(Fighter fighterPlaying)
        {
            foreach (var rune in fighterPlaying.GetMarks<Rune>().ToArray())
            {
                if (rune.DecrementDuration())
                {
                    RemoveMark(rune);
                }
            }
            foreach (var glyph in fighterPlaying.GetMarks<Glyph>().ToArray())
            {
                if (glyph.DecrementDuration())
                {
                    RemoveMark(glyph);
                }
            }
        }


        public void AddSummon(Fighter source, SummonedFighter fighter)
        {
            AddSummons(source, new SummonedFighter[] { fighter });
        }
        public void AddSummons(Fighter source, IEnumerable<SummonedFighter> summons)
        {
            foreach (var summon in summons)
            {
                source.Team.AddFighter(summon);
                Timeline.InsertFighter(summon, Timeline.Index + 1);
                summon.Initialize();
            }

            foreach (var target in GetAllConnectedFighters())
            {
                target.Send(new GameActionFightSummonMessage(summons.Select(x => x.GetFightFighterInformations(target)).ToArray(),
                0, source.Id));
            }


            this.UpdateTimeLine();

            foreach (var summon in summons)
            {
                summon.OnSummoned();
            }

            source.TriggerBuffs(TriggerTypeEnum.OnSummon, null);

        }

        public IEnumerable<Buff> GetAllBuffs()
        {
            return Buffs;
        }
        public void UpdateRound()
        {
            this.Send(new GameFightNewRoundMessage(Timeline.RoundNumber));
        }
        public void Synchronize()
        {
            foreach (var fighter in GetAllConnectedFighters())
            {
                Synchronize(fighter);
            }
        }
        public void Synchronize(CharacterFighter fighter)
        {
            fighter.Send(new GameFightSynchronizeMessage(GetFighters().Select(x => x.GetFightFighterInformations(fighter)).ToArray()));
        }




        public void UpdateTimeLine()
        {
            foreach (var fighter in GetAllConnectedFighters())
            {
                UpdateTimeLine(fighter);
            }
        }
        public void UpdateTimeLine(CharacterFighter fighter)
        {
            double[] ids = this.Timeline.GetAlives().Where(x => x.DisplayInTimeline()).Select(x => (double)x.Id).ToArray();
            double[] deads = this.Timeline.GetDeads().Where(x => x.DisplayInTimeline()).Select(x => (double)x.Id).ToArray();
            fighter.Send(new GameFightTurnListMessage(ids, deads));
        }

        public void CheckFightStart()
        {
            if (this.RedTeam.AreAllReady() && this.BlueTeam.AreAllReady())
            {
                this.StartFighting();
            }
        }
        public virtual bool CheckFightEnd()
        {
            if (BlueTeam.Alives == 0 || RedTeam.Alives == 0 && !Ended)
            {
                Ended = true;

                if (Started)
                {
                    if (Synchronizer != null)
                    {
                        Synchronizer.Cancel();
                        Synchronizer = null;
                    }

                    if (SequenceManager.IsSequencing)
                        SequenceManager.EndAllSequences();

                    Synchronizer = Synchronizer.RequestCheck(SynchronizerRole.EndFight, this, EndFight, delegate (CharacterFighter[] actors)
                     {
                         EndFight();
                     }, Fight.SynchronizerTimout * 1000);

                }
                else
                    EndFight();

            }
            return Ended;
        }
        public void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            fighter.Character.Client.Send(new GameFightJoinMessage(true, !Started, false, Started, GetPlacementTimeLeft(), (byte)FightType));
        }

        public abstract FightCommonInformations GetFightCommonInformations();

        public FightTeamInformations[] GetFightTeamInformations()
        {
            return new FightTeamInformations[2] {RedTeam.GetFightTeamInformations(),
                BlueTeam.GetFightTeamInformations()};
        }
        public FightOptionsInformations[] GetFightOptionsInformations()
        {
            return new FightOptionsInformations[]
            {
                    RedTeam.Options.GetFightOptionsInformations(),
                    BlueTeam.Options.GetFightOptionsInformations()
            };
        }

        private void DeterminsWinners()
        {
            if (this.BlueTeam.Alives == 0)
            {
                this.Winners = this.RedTeam;
            }
            else if (this.RedTeam.Alives == 0)
            {
                this.Winners = this.BlueTeam;
            }

            OnWinnersDetermined();
        }

        protected virtual void OnWinnersDetermined()
        {

        }

        public virtual void EndFight()
        {
            if (Started)
            {
                this.DeterminsWinners();

                this.Synchronizer = null;

                IEnumerable<IFightResult> results = this.GenerateResults();

                this.ApplyResults(results);

                this.Send(new GameFightEndMessage(GetFightDuration(), 1, 0, (from entry in results
                                                                             select entry.GetFightResultListEntry()).ToArray(),
                                                                                    new NamedPartyTeamWithOutcome[0]));
            }

            long targetMapId = TargetMapId.HasValue ? TargetMapId.Value : Map.Id;

            foreach (CharacterFighter current in this.GetFighters<CharacterFighter>(false))
            {
                bool winner = current.Team == Winners ? true : false;

                current.Character.Record.FightId = null;

                current.Character.RejoinMap(targetMapId, FightType, winner, SpawnJoin);
            }

            OnFightEnded();

            Dispose();

        }
        protected abstract IEnumerable<IFightResult> GenerateResults();

        protected void ApplyResults(IEnumerable<IFightResult> results)
        {
            foreach (IFightResult current in results)
            {
                current.Apply();
            }
        }
        public int GetFightDuration()
        {
            return (!this.StartAcknowledged) ? 0 : ((int)(System.DateTime.Now - this.StartTime.Value).TotalMilliseconds);
        }

        public void Join(Character character, double leaderId)
        {
            FightTeam joinedTeam;

            if (BlueTeam.Leader.Id == leaderId)
                joinedTeam = BlueTeam;
            else if (RedTeam.Leader.Id == leaderId)
                joinedTeam = RedTeam;
            else
            {
                character.ReplyError("Unable to find a team to join...");
                return;
            }

            if (joinedTeam.Options.CanJoin(character))
            {
                joinedTeam.AddFighter(character.CreateFighter(joinedTeam));
            }
        }
        public bool CanBeSeen(MapPoint from, MapPoint to, bool throughEntities = false, Fighter except = null)
        {
            if (from == null || to == null) return false;
            if (from == to)
                return true;

            var occupiedCells = new short[0];
            if (!throughEntities)
                occupiedCells = GetFighters<Fighter>().Where(x => x != except && x.BlockLOS()).Select(x => x.Cell.Id).ToArray();

            var line = new LineSet(from, to);
            return !(from point in line.EnumerateValidPoints().Skip(1)
                     where to.CellId != point.CellId
                     let cell = Map.Cells[point.CellId]
                     where !cell.LineOfSight || !throughEntities && Array.IndexOf(occupiedCells, point.CellId) != -1
                     select point).Any();
        }

        public abstract void OnFightEnded();

        public void OnTimeout()
        {
            GetTeam(TeamTypeEnum.TEAM_TYPE_PLAYER).KillTeam();
        }
        public void Dispose()
        {
            if (PlacementTimer != null)
            {
                PlacementTimer.Dispose();
                PlacementTimer = null;
            }

            if (m_turnTimer != null)
            {
                m_turnTimer.Dispose();
                m_turnTimer = null;
            }

            if (Synchronizer != null)
            {
                Synchronizer.Cancel();
                Synchronizer = null;
            }

            this.RedTeam = null;
            this.BlueTeam = null;
            Map.Instance.RemoveFight(this);
            FightManager.Instance.RemoveFight(this);
        }

        public bool ContainsBoss()
        {
            var monsterTeam = GetTeam(TeamTypeEnum.TEAM_TYPE_MONSTER);

            if (monsterTeam == null)
            {
                return false;
            }
            return monsterTeam.GetFighters<MonsterFighter>().Any(x => x.Record.IsBoss);
        }
    }
}
