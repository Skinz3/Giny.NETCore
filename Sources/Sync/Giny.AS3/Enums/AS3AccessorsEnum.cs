using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.AS3.Enums
{
    [Flags]
    public enum AS3AccessorsEnum : byte
    {
        None = 0x0,
        @public = 0x1,
        @private = 0x2,
        @protected = 0x4,
    }
}
