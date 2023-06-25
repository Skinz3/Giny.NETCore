using Giny.Core.DesignPattern;
using Giny.Core.Logging;
using Giny.Core.Network.Messages;
using Giny.World.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Logging
{
    public class LogManager : Singleton<LogManager>
    {
        private const string Path = "logs.txt";

        private LogFile File
        {
            get;
            set;
        }

        [StartupInvoke("Logs", StartupInvokePriority.SixthPath)]
        public void Initialize()
        {
            File = new LogFile(Path);
        }

        public void OnError(WorldClient client, NetworkMessage message, Exception ex)
        {
            if (client == null || message == null)
            {
                return;
            }

            string source = "Unknown";
            long mapId = -1;

            if (client.Ip != null)
            {
                source = client.Ip;
            }
            if (client.Character != null)
            {
                source = client.Character.Name;
                mapId = client.Character.Map.Id;
            }

            lock (File)
            {
                File.AppendError(source, mapId, message, ex);
            }
        }
    }
}
