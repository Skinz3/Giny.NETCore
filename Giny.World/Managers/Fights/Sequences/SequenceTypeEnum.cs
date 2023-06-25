using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Fights.Sequences
{
    public enum SequenceTypeEnum
    {
        SEQUENCE_SPELL = 1,
        SEQUENCE_WEAPON = 2,
        SEQUENCE_GLYPH_TRAP = 3,
        SEQUENCE_TRIGGERED = 4,
        SEQUENCE_MOVE = 5,
        SEQUENCE_CHARACTER_DEATH = 6,
        SEQUENCE_TURN_START = 7,
        SEQUENCE_TURN_END = 8,
        SEQUENCE_FIGHT_START = 9,
    }
}
