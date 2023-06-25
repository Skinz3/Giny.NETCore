using Giny.Core;
using Giny.Core.DesignPattern;
using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using Giny.World.Managers.Effects;
using Giny.World.Managers.Entities.Characters;
using Giny.World.Modules;
using Giny.World.Records.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Items
{
    public class ItemEffectAttribute : Attribute
    {
        public EffectsEnum Effect
        {
            get;
            set;
        }
        public ItemEffectAttribute(EffectsEnum effect)
        {
            this.Effect = effect;
        }
    }
    public class ItemEffectsManager : Singleton<ItemEffectsManager>
    {
        private readonly Dictionary<EffectsEnum, MethodInfo> Handlers = new Dictionary<EffectsEnum, MethodInfo>();

        [StartupInvoke("Item effects", StartupInvokePriority.FourthPass)]
        public void Initialize()
        {
            foreach (var type in AssemblyCore.GetTypes())
            {
                foreach (var method in type.GetMethods())
                {
                    var attribute = method.GetCustomAttribute<ItemEffectAttribute>();

                    if (attribute != null)
                    {
                        Handlers.Add(attribute.Effect, method);
                    }
                }
            }

        }

        public void AddEffects(Character character, EffectCollection effects)
        {
            foreach (var effect in effects.OfType<EffectInteger>())
            {
                if (Handlers.ContainsKey(effect.EffectEnum))
                {
                    Handlers[effect.EffectEnum].Invoke(null, new object[] { character, effect.Value });
                }
                else
                {
                    if (character.Client.Account.Role == ServerRoleEnum.Administrator)
                        character.ReplyWarning("Unknown item effect handler :" + effect.EffectEnum);
                }
            }
        }

        public void RemoveEffects(Character character, EffectCollection effects)
        {
            foreach (var effect in effects.OfType<EffectInteger>())
            {
                if (Handlers.ContainsKey(effect.EffectEnum))
                {
                    Handlers[effect.EffectEnum].Invoke(null, new object[] { character, -effect.Value });
                }
            }
        }
    }
}
