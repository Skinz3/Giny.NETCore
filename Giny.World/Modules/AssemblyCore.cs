using Giny.World.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Modules
{
    public class AssemblyCore
    {
        private static IEnumerable<Type> m_types = null;

        public static void OnAssembliesLoaded()
        {
            var assemblyType = Assembly.GetAssembly(typeof(AssemblyCore)).GetTypes();
            var modulesTypes = ModuleManager.Instance.GetModuleTypes();
            m_types = assemblyType.Concat(modulesTypes);
        }
        public static IEnumerable<Type> GetTypes()
        {
            return m_types;
        }
    }
}
