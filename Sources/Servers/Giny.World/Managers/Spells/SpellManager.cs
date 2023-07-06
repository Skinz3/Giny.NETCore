using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Spells
{
    public class SpellManager : Singleton<SpellManager>
    {
        private readonly Dictionary<short, Type> m_handlers = new Dictionary<short, Type>();

        [StartupInvoke(StartupInvokePriority.FourthPass)]
        public void Initialize()
        {
            foreach (var type in AssemblyCore.GetTypes())
            {
                foreach (var attribute in type.GetCustomAttributes<SpellCastHandlerAttribute>())
                {
                    m_handlers.Add(attribute.SpellId, type);
                }
            }
        }
        public SpellCastHandler CreateSpellCastHandler(SpellCast cast)
        {
            if (m_handlers.ContainsKey(cast.SpellId))
            {
                return (SpellCastHandler)Activator.CreateInstance(m_handlers[cast.SpellId], cast);
            }

            return new DefaultSpellCastHandler(cast);
        }
    }
}
