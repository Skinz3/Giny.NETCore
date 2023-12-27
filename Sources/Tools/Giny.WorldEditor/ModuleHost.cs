using Giny.Core.DesignPattern;
using Giny.World.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.WorldEditor
{
    public class ModuleHost : Singleton<ModuleHost>
    {
        private Dictionary<Type, IModule> Modules = new Dictionary<Type, IModule>();

        public T InitModule<T>() where T : IModule
        {
            if (Modules.ContainsKey(typeof(T)))
            {
                return (T)Modules[typeof(T)];
            }
            else
            {
                var instance = (IModule)Activator.CreateInstance(typeof(T));
                instance.Initialize();
                Modules.Add(typeof(T), instance);
                return (T)instance;

            }
        }
    }
}
