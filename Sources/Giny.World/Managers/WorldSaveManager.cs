using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.Time;
using Giny.ORM;
using Giny.ORM.Cyclic;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Giny.World.Managers
{
    public class WorldSaveManager : Singleton<WorldSaveManager>
    {
        public const double SAVE_INTERVAL_MINUTES = 30;

        private ActionTimer m_callback;

        [StartupInvoke("Save Task", StartupInvokePriority.Last)]
        public void CreateNextTask()
        {
            m_callback = new ActionTimer((int)((SAVE_INTERVAL_MINUTES * 60) * 1000), PerformSave, true);
            m_callback.Start();
        }


        public void PerformSave()
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                if (WorldServer.Instance.Status == ServerStatusEnum.ONLINE)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    WorldServer.Instance.SetServerStatus(ServerStatusEnum.SAVING);

                    //   WorldServer.Instance.Foreach(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 164));

                    foreach (var client in WorldServer.Instance.GetClients().Where(x => x.Character != null))
                    {
                        client.Character.Record.UpdateElement();
                    }

                    CyclicSaveTask.Instance.Save();

                    //    WorldServer.Instance.Foreach(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 165));

                    WorldServer.Instance.SetServerStatus(ServerStatusEnum.ONLINE);

                    Logger.WriteColor2("World server saved in " + Math.Round(stopwatch.Elapsed.TotalSeconds, 2) + "s");
                }
                else
                {
                    Logger.Write("Unable to save world server, server is busy...", Channels.Warning);
                }
            }));


            thread.Start();


        }

    }
}
