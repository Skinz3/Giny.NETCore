using Giny.Core.Misc;
using Giny.World.Managers.Fights;
using Giny.World.Network;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Giny.World.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommonController : ControllerBase
    {
        [Route("stats")]
        public object GetStats()
        {
            Process currentProcess = Process.GetCurrentProcess();
            // PrivateMemorySize64 represents the private memory usage in bytes
            long memoryUsage = currentProcess.PrivateMemorySize64;
            currentProcess.Dispose();

            object result = new
            {
                Online = WorldServer.Instance.GetOnlineClients().Count(),
                OnlineIps = WorldServer.Instance.GetOnlineClients().DistinctBy(x => x.Ip).Count(),
                Peak = WorldServer.Instance.MaximumClients,
                Fights = FightManager.Instance.GetFightCount(),
                Memory = MemoryFormatter.FormatBytes(memoryUsage),
            };

            return result;
        }
    }
}