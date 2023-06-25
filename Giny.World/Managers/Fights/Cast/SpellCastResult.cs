using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Cast
{
    public enum SpellCastResult
    {
        NO_LOS,
        HISTORY_ERROR,
        NOT_IN_ZONE,
        STATE_REQUIRED,
        STATE_FORBIDDEN,
        CELL_NOT_FREE,
        NOT_ENOUGH_AP,
        UNWALKABLE_CELL,
        HAS_NOT_SPELL,
        CANNOT_PLAY,
        UNKNOWN,
        OK,
    }
}
