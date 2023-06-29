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

        public override CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterUsableCharacteristicDetailed(Used, (short)characteristic, Base, Additional, Objects,
                0, Context);
        }
    }
}
