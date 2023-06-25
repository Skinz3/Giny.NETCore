using Giny.Core.Network.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.Logging
{
    public class LogFile
    {
        private static string ErrorLine = "{0} Source : {1} MapId : {2} Message : {3} Exception : {4}" + Environment.NewLine + Environment.NewLine;

        private string Path
        {
            get;
            set;
        }
        public LogFile(string path)
        {
            this.Path = path;

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public void AppendError(string source, long mapId, NetworkMessage message, Exception ex)
        {
            string value = string.Format(ErrorLine, DateTime.UtcNow, source, mapId, message.GetType().Name, ex);
            File.AppendAllText(Path, value);
        }
    }
}
