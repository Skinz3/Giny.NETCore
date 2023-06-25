using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Modules
{
    public interface IModule 
    {
        void Initialize();

        void CreateHooks();
    }
}
