using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.Core.IO.Configuration
{
    public interface IConfigFile
    {
        void OnLoaded();

        void OnCreated();

    }
}
