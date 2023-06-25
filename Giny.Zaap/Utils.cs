using Giny.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Zaap
{
    public class Utils
    {
        public static void StartClient(string clientPath, int port, int instanceId)
        {
            var dofusPath = Path.Combine(clientPath, ClientConstants.ExePath);
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = dofusPath;
            ps.Arguments = string.Format("--port={0} --gameName=dofus --gameRelease=main --instanceId={1} --hash=464e4625-67f1-4706-985c-8358f8661e3c --canLogin=true", port, instanceId);
            Process process = new Process();
            process.StartInfo = ps;
            process.Start();
        }
    }
}
