using Giny.World.Api;
using Giny.World.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AdditionalDrop
{
    [Module("Additional Drop")]
    public class Module : IModule
    {
        public void CreateHooks()
        {
            FightEventApi.OnPlayerResultApplied += DropManager.Instance.OnPlayerResultApplied;
        }

        public void Initialize()
        {
            DropManager.Instance.Initialize();
        }
    }
}
