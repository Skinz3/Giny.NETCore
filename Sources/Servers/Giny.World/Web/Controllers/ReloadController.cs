using Giny.World.Managers.Maps.Npcs;
using Giny.World.Network;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReloadController : ControllerBase
    {
        [Route("npcs")]
        public bool ReloadNpcs()
        {
            try
            {
                NpcsManager.Instance.ReloadNpcs();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
