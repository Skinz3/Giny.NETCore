using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SpellEffectHandlerAttribute : Attribute
    {
        public EffectsEnum Effect
        {
            get;
            set;
        }

        public SpellEffectHandlerAttribute(EffectsEnum effect)
        {
            this.Effect = effect;
        }

    }
}
