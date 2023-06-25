using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Time;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Managers.Fights.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Synchronisation
{
    public class Synchronizer
    {
        private int timeout;
        private List<CharacterFighter> m_fighters;
        private readonly Fight m_fight;
        private bool m_started;
        private ActionTimer m_timer;
        public event System.Action<Synchronizer> Success;
        public event Action<Synchronizer, CharacterFighter[]> Timeout;

        /*
        * Maximum time the fight can live without players.
        * 5 minutes timeout
        */
        public const int VoidTimeout = 5 * 60;
        /*
         * 5 seconds
         */
        public const int VoidRefreshDelay = 5;

        private int voidTimeout;
        private ActionTimer voidTimer;

        public SynchronizerRole Role
        {
            get;
            private set;
        }

        private void NotifySuccess()
        {
            this.Success?.Invoke(this);
        }
        private void NotifyTimeout(CharacterFighter[] laggers)
        {
            this.Timeout?.Invoke(this, laggers);
        }
        public Synchronizer(SynchronizerRole role, Fight fight, IEnumerable<CharacterFighter> actors, int timeout)
        {
            this.Role = role;
            this.m_fight = fight;
            this.m_fighters = actors.ToList<CharacterFighter>();
            this.timeout = timeout;
        }
        private void VoidFight()
        {
            this.voidTimer = new ActionTimer(VoidRefreshDelay * 1000, VoidLoop, true);
            voidTimer.Start();
        }
        private void VoidLoop()
        {
            if (m_fight.GetAllConnectedFighters().Count() > 0)
            {
                voidTimer.Dispose();
                m_fighters = m_fight.GetAllConnectedFighters().ToList();
                Start();
                return;
            }

            voidTimeout += VoidRefreshDelay;

            if (voidTimeout >= VoidTimeout)
            {
                voidTimer.Dispose();
                m_fight.GetTeam(TeamTypeEnum.TEAM_TYPE_PLAYER).KillTeam();
                m_fight.EndFight();
                return;
            }
        }

        public void Start()
        {
            if (!this.m_started)
            {
                if (m_fighters.Count == 0)
                {
                    VoidFight();
                    return;
                }

                this.m_started = true;

                if (this.m_fighters.Count == 0)
                {
                    NotifySuccess();
                }
                else
                {
                    foreach (CharacterFighter current in this.m_fighters)
                    {
                        current.Character.Client.Send(new GameFightTurnReadyRequestMessage(m_fight.FighterPlaying.Id));
                    }

                    this.m_timer = new ActionTimer(timeout, this.TimedOut, false);
                    this.m_timer.Start();
                }
            }

        }

        public void Cancel()
        {
            this.m_started = false;
            if (this.m_timer != null)
            {
                this.m_timer.Dispose();
            }
        }
        public void ToggleReady(CharacterFighter actor, bool ready = true)
        {
            if (this.m_started)
            {
                if (ready && this.m_fighters.Contains(actor))
                {
                    this.m_fighters.Remove(actor);
                }
                else
                {
                    if (!ready)
                    {
                        this.m_fighters.Add(actor);
                    }
                }
                if (this.m_fighters.Count == 0)
                {
                    if (this.m_timer != null)
                    {
                        this.m_timer.Dispose();

                    }
                    this.NotifySuccess();
                }
            }
        }
        private void TimedOut()
        {
            if (this.m_started && this.m_fighters.Count > 0)
            {
                this.NotifyTimeout(this.m_fighters.ToArray());
            }
        }
        public static Synchronizer RequestCheck(SynchronizerRole role, Fight fight, Action success, System.Action<CharacterFighter[]> fail, int timeout)
        {
            IEnumerable<CharacterFighter> fighters = fight.GetAllConnectedFighters();

            return RequestCheck(role, fight, success, fighters, fail, timeout);

        }
        public static Synchronizer RequestCheck(SynchronizerRole role, Fight fight, Action success, IEnumerable<CharacterFighter> fighters, System.Action<CharacterFighter[]> fail, int timeout)
        {
            Synchronizer readyChecker = new Synchronizer(role, fight, fighters, timeout);
            readyChecker.Success += delegate (Synchronizer chk)
            {
                success();
            };
            readyChecker.Timeout += delegate (Synchronizer chk, CharacterFighter[] laggers)
            {
                fail(laggers);
            };
            readyChecker.Start();
            return readyChecker;
        }
    }
    public enum SynchronizerRole
    {
        CharacterLeave,
        EndTurn,
        EndFight,
        StartFight,
    }
}
