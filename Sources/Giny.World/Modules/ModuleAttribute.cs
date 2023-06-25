using Giny.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleAttribute : Attribute
    {
        public string ModuleName
        {
            get;
            set;
        }

        public ModuleAttribute(string moduleName)
        {
            this.ModuleName = moduleName;
        }
    }
}
