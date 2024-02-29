using Giny.Core.DesignPattern;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Fights.Cast;
using Giny.World.Managers.Fights.Fighters;
using Giny.World.Modules;
using Giny.World.Records.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Effects
{
    public class SpellEffectManager : Singleton<SpellEffectManager>
    {
        private static readonly Dictionary<EffectsEnum, Type> m_handlers = new Dictionary<EffectsEnum, Type>();

        [StartupInvoke(StartupInvokePriority.FifthPass)]
        public void Initialize()
        {
            m_handlers.Clear();

            foreach (var type in AssemblyCore.GetTypes())
            {
                SpellEffectHandlerAttribute[] handlers = type.GetCustomAttributes<SpellEffectHandlerAttribute>().ToArray();

                foreach (var handler in handlers)
                {
                    m_handlers.Add(handler.Effect, type);
                }
            }
        }
        public bool Exists(EffectsEnum effect)
        {
            return m_handlers.ContainsKey(effect);
        }
        private Type GetHandlerType(EffectsEnum effectEnum)
        {
            Type type = null;

            if (!m_handlers.TryGetValue(effectEnum, out type))
            {
                return null;
            }

            return type;
        }
        public SpellEffectHandler GetSpellEffectHandler(Effect effect, SpellCastHandler castHandler)
        {
            var type = GetHandlerType(effect.EffectEnum);

            if (type == null)
            {
                return null;
            }
            return (SpellEffectHandler)Activator.CreateInstance(type, new object[] { effect, castHandler });
        }
    }
}
