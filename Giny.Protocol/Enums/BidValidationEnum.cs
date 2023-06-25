






using System;

namespace Giny.Protocol.Enums
{
    public enum BidValidationEnum
    {
        GENERIC_ERROR = 0,
        BUFFER_OVERLOAD = 1,
        OFFER_DOESNT_EXIST = 2,
        OFFER_ALREADY_EXISTS = 3,
        NOT_ENOUGH_KAMAS = 4,
        NOT_ENOUGH_OGRINES = 5,
        SERVER_MAINTENANCE = 6,
        PLAYER_IN_DEBT = 7,
        OFFER_IS_YOURS = 8,
        VALIDATION_SUCCESS = 100,

    }
}

