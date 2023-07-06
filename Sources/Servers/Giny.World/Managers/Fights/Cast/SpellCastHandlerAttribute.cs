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
        public short SpellId
        {
            get;
            set;
        }
        public SpellCastHandlerAttribute(short spellId)
        {
            this.SpellId = spellId;
        }
    }
}
