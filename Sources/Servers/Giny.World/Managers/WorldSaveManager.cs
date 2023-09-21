using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Core.Extensions;
using Giny.Core.IO.Configuration;
using Giny.Core.Time;
using Giny.ORM;
using Giny.ORM.Cyclic;
using Giny.Protocol.Enums;
using Giny.Protocol.Messages;
using Giny.World.Logging;
using Giny.World.Network;
using Giny.World.Records;
using Giny.World.Records.Characters;
using Giny.World.Records.Items;
using Giny.World.Records.Spells;
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
        private ActionTimer CallbackTimer
        {
            get;
            set;
        }

        [StartupInvoke("Periodic persistence", StartupInvokePriority.Last)]
        public void Initialize()
        {
            CallbackTimer = new ActionTimer((int)(ConfigManager<WorldConfig>.Instance.SaveIntervalMinutes * 60 * 1000), PerformSave, true); ;
            CallbackTimer.Start();
        }


        public void PerformSave()
        {
            if (WorldServer.Instance.GetServerStatus() == ServerStatusEnum.ONLINE)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                WorldServer.Instance.SetServerStatus(ServerStatusEnum.SAVING);

                //WorldServer.Instance.Foreach(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 164));

                foreach (var client in WorldServer.Instance.GetClients().Where(x => x.Character != null))
                {
                    client.Character.Record.UpdateLater();
                }

                try
                {
                    CyclicSaveTask.Instance.Save();
                }
                catch (Exception ex)
                {
                    LogManager.Instance.AppendError("Unable to save worldserver", ex);
                    Logger.Write("Unable to save worldserver : " + ex, Channels.Critical);
                }

                // WorldServer.Instance.Foreach(x => x.Character.TextInformation(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 165));

                WorldServer.Instance.SetServerStatus(ServerStatusEnum.ONLINE);

                Logger.WriteColor2("World server saved in " + Math.Round(stopwatch.Elapsed.TotalSeconds, 2) + "s");
            }
            else
            {
                Logger.Write("Unable to save world server, server is busy...", Channels.Warning);
            }
        }

    }
}
