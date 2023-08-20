using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    public class LifeLossCharacteristic : Characteristic
    {
        public override bool FightCallback => false;

        private LifeCharacteristic Life
        {
            get;
            set;
        }
        public LifeLossCharacteristic(int @base) : base(@base)
        {

        }

        public LifeLossCharacteristic()
        {

        }
        public override int Context
        {
            get
            {
                return base.Context;
            }
            set
            {
                base.Context = value;
            }

        }


        public void Bind(LifeCharacteristic life)
        {
            this.Life = life;
        }


        public override CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterCharacteristicDetailed(-Life.Total(), 0, 0, 0, 0, (short)characteristic);
            var value = -TotalInContext() + Life.Eroded;
            return new CharacterCharacteristicValue(value, (short)characteristic);
        }
        public static LifeLossCharacteristic New()
        {
            return new LifeLossCharacteristic(0)
            {
                Context = 0,
            };
        }

        public override Characteristic Clone()
        {
            return new LifeLossCharacteristic(Base)
            {

            };
        }
    }
}
