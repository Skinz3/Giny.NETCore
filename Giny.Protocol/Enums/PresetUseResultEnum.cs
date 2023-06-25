






using System;

namespace Giny.Protocol.Enums
{
    public enum PresetUseResultEnum
    {
        PRESET_USE_OK = 1,
        PRESET_USE_OK_PARTIAL = 2,
        PRESET_USE_ERR_STATS_FIGHT_PREPARATION = 3,
        PRESET_USE_ERR_COOLDOWN = 4,
        PRESET_USE_ERR_BAD_PRESET_ID = 5,
        PRESET_USE_ERR_INVALID_STATE = 6,
        PRESET_USE_ERR_STATS = 7,
        PRESET_USE_ERR_CRITERION = 8,
        PRESET_USE_ERR_UNKNOWN = 9,
        PRESET_USE_ERR_INVALID_DATA = 10,

    }
}

