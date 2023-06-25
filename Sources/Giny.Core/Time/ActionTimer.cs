using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Giny.Core.Time
{
    public class ActionTimer : IDisposable
    {
        private Action m_action;
        private Timer m_timer;

        public bool Started
        {
            get;
            private set;
        }
        public ActionTimer(int interval, Action action, bool loop)
        {
            this.m_action = action;
            this.m_timer = new Timer(interval);
            this.m_timer.Elapsed += m_timer_Elapsed;
            this.m_timer.AutoReset = loop;
        }
        /// <summary>
        /// En secondes
        /// </summary>
        public double Interval
        {
            get
            {
                return m_timer.Interval;
            }
            set
            {
                m_timer.Interval = value * 1000;
            }
        }
        void m_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m_action();

            if (m_timer != null && !m_timer.AutoReset)
            {
                Dispose();
            }
        }

        /*
         * Start on the thread pool.
         */
        public void Start()
        {
            if (m_action == null)
            {
                throw new Exception("Unable to start timer. Action is null, maybe timer is disposed?");
            }
            m_timer.Start();
            Started = true;
        }
        public void Pause()
        {
            Started = false;
            m_timer.Stop();
        }

        public void Dispose()
        {
            Started = false;
            m_timer?.Stop();
            m_timer?.Dispose();
            m_timer = null;
            m_action = null;
        }
    }
}
