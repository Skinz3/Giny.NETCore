using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3.Enums
{
    [Flags]
    public enum AS3ModifiersEnum : byte
    {
        None = 0x0,
        @const = 0x2,
        @static = 0x4,
        @override = 0x8,
        @virtual = 0x16,
    }
}
