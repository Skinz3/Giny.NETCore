using Giny.Core;
using Giny.Core.DesignPattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Modules
{
    public class ModuleManager : Singleton<ModuleManager>
    {
        private const string ModulesPath = "Modules\\";

        private const string Extension = ".dll";

        private readonly Dictionary<string, IModule> m_modules = new Dictionary<string, IModule>();

        private readonly List<Type> m_modulesTypes = new List<Type>();

        [StartupInvoke("Modules", StartupInvokePriority.Initial)]
        public void Initialize()
        {

            string path = Path.Combine(Environment.CurrentDirectory, ModulesPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var file in Directory.GetFiles(path))
            {
                if (Path.GetExtension(file).ToLower() == Extension)
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    IEnumerable<Type> types = assembly.GetTypes();

                    m_modulesTypes.AddRange(types);

                    foreach (var type in types)
                    {
                        if (type.GetCustomAttribute<ModuleAttribute>() != null)
                        {
                            if (type.GetInterfaces().Contains(typeof(IModule)))
                            {
                                LoadModule(type);
                            }
                        }
                    }
                }
            }

            AssemblyCore.OnAssembliesLoaded();
        }

        [StartupInvoke("Modules", StartupInvokePriority.Modules)]
        public void LoadModules()
        {
            foreach (var module in m_modules)
            {
                Logger.WriteColor2("Applying '" + module.Key + "'");
                module.Value.Initialize();
                module.Value.CreateHooks();
            }
        }
       

        public IEnumerable<Type> GetModuleTypes()
        {
            return m_modulesTypes;
        }
        private void LoadModule(Type type)
        {
            string moduleName = type.GetCustomAttribute<ModuleAttribute>().ModuleName;
            IModule module = (IModule)Activator.CreateInstance(type);
            Logger.Write("Module '" + moduleName + "' loaded", Channels.Log);
            m_modules.Add(moduleName, module);
        }
    }
}
