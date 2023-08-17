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

        private static string ClientErrorLine = "{0} Source : {1} MapId : {2} Message : {3} Exception : {4}" + Environment.NewLine + Environment.NewLine;

        static object _lock = new object();

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


        public void AppendError(WorldClient client, NetworkMessage message, Exception ex)
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
            }

            if (client.Character.Map != null)
            {
                mapId = client.Character.Map.Id;
            }

            string content = string.Format(ClientErrorLine, DateTime.UtcNow, source, mapId, message.GetType().Name, ex);
            File.AppendError(content);

        }
        public void AppendError(string message, Exception ex)
        {
            string content = message + " : " + ex;
            File.AppendError(content);
        }
    }
}
