






using System;

namespace Giny.Protocol.Enums
{
    public enum NicknameErrorEnum
    {
        ALREADY_USED = 1,
        SAME_AS_LOGIN = 2,
        TOO_SIMILAR_TO_LOGIN = 3,
        INVALID_NICK = 4,
        UNKNOWN_NICK_ERROR = 99,

    }
}

