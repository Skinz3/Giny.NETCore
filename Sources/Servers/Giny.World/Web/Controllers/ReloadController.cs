using Giny.World.Managers.Items;
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
                NpcsManager.Instance.Reload();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [Route("items")]
        public bool ReloadItems()
        {
            try
            {
                ItemsManager.Instance.Reload();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
