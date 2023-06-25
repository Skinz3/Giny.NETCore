using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Spells
{
    [Flags]
    public enum SpellCategoryEnum
    {
        None = 0,
        Agressive = 1,
        Healing = 2,
        Teleport = 4,
        Buff = 8,
        Summon = 16,
        Mark = 32,
        Debuff = 64,
    }
}
