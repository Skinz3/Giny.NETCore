using Giny.Auth.Records;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.IPC.Types;
using Giny.World.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Auth.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        [Route("launcher")]
        public string GetLauncherVersion()
        {
            return "1.0.0";
        }
    }
}
