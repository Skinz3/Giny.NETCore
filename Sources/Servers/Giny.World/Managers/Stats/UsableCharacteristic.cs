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

        public UsableCharacteristic()
        {

        }
        public UsableCharacteristic(short @base) : base(@base)
        {

        }
        private short m_used;

        public short Used
        {
            get
            {
                return m_used;
            }
            set
            {
                m_used = value;
            }
        }

        public override CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterUsableCharacteristicDetailed(Math.Abs(Used), (short)characteristic, Base, Additional, Objects,
                0, Context);
        }
    }
}
