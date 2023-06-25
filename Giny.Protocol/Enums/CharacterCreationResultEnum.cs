






using System;

namespace Giny.Protocol.Enums
{
    public enum CharacterCreationResultEnum
    {
        OK = 0,
        ERR_NO_REASON = 1,
        ERR_INVALID_NAME = 2,
        ERR_NAME_ALREADY_EXISTS = 3,
        ERR_TOO_MANY_CHARACTERS = 4,
        ERR_NOT_ALLOWED = 5,
        ERR_NEW_PLAYER_NOT_ALLOWED = 6,
        ERR_RESTRICED_ZONE = 7,
        ERR_INCONSISTENT_COMMUNITY = 8,

    }
}

