using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Bidshops
{
    public class AveragePriceScheduler
    {
        private static Task m_task;

        public const double RefreshDelaySeconds = 120f;

        [StartupInvoke("Average Price Scheduler", StartupInvokePriority.Last)]
        public static void CreateTask()
        {
            m_task = Task.Factory.StartNewDelayed((int)(RefreshDelaySeconds * 1000), RefreshAveragePrices);
        }
        private static void RefreshAveragePrices()
        {
            BidshopsManager.Instance.RefreshAveragePrices();
            CreateTask();
        }
    }
}
