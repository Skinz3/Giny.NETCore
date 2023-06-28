using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    public abstract class UsableCharacteristic : DetailedCharacteristic
    {
        public short Used
        {
            get;
            set;
        }

        public override short Total()
        {
            var total = (short)(Base + Additional + Objects - Used);

            if (!Limit.HasValue)
            {
                return total;
            }
            return total > Limit.Value ? Limit.Value : total;
        }
        public override short TotalInContext()
        {
            var totalContext = (short)(Total() + Context - Used);

            if (ContextualLimit && Limit.HasValue)
            {
                return totalContext > Limit.Value ? Limit.Value : totalContext;
            }
            else
            {
                return totalContext;
            }
        }
        public override CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterUsableCharacteristicDetailed(Used, (short)characteristic, Base, Additional, Objects,
                0, Context);
        }
    }
}
