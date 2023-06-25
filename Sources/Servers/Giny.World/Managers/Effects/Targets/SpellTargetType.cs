using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Effects.Targets
{
    [Flags]
    public enum SpellTargetType
    {
        NONE = 0,
        SELF_ONLY = 0x80000, // C
        SELF = 0x1, // c

        ALLY_MONSTER_SUMMON = 0x2, // s
        ALLY_SUMMON = 0x4, // j
        ALLY_NON_MONSTER_SUMMON = 0x8, // i
        ALLY_COMPANION = 0x10, // d
        ALLY_MONSTER = 0x20, // m
        ALLY_SUMMONER = 0x40, // h
        ALLY_PLAYER = 0x80, // l

        ALLY_ALL_EXCEPT_SELF = ALLY_MONSTER_SUMMON | ALLY_SUMMON | ALLY_NON_MONSTER_SUMMON |
            ALLY_COMPANION | ALLY_COMPANION | ALLY_MONSTER | ALLY_PLAYER, // g

        ALLY_ALL = SELF | ALLY_MONSTER_SUMMON | ALLY_SUMMON | ALLY_NON_MONSTER_SUMMON |
            ALLY_COMPANION | ALLY_COMPANION | ALLY_MONSTER | ALLY_PLAYER, // a

        ENEMY_MONSTER_SUMMON = 0x100, // S
        ENEMY_SUMMON = 0x200, // J
        ENEMY_NON_MONSTER_SUMMON = 0x400, // I
        ENEMY_COMPANION = 0x800, // D
        ENEMY_MONSTER = 0x1000, // M
        ENEMY_HUMAN = 0x2000, // H
        ENEMY_PLAYER = 0x4000, // L

        ENEMY_ALL = ENEMY_MONSTER_SUMMON | ENEMY_SUMMON | ENEMY_NON_MONSTER_SUMMON |
            ENEMY_COMPANION | ENEMY_COMPANION | ENEMY_MONSTER | ENEMY_HUMAN | ENEMY_PLAYER // A
    }
}
