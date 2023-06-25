using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SpellCastHandlerAttribute : Attribute
    {
        public SpellEnum SpellEnum
        {
            get;
            set;
        }
        public SpellCastHandlerAttribute(SpellEnum spell)
        {
            this.SpellEnum = spell;
        }
    }
}
