using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class LifeCharacteristic : DetailedCharacteristic
    {
        [ProtoMember(1)]
        public override int Base { get => base.Base; set => base.Base = value; }

        [ProtoMember(2)]
        public override int Additional { get => base.Additional; set => base.Additional = value; }

        public override int Objects { get => base.Objects; set => base.Objects = value; }

        private DetailedCharacteristic Vitality
        {
            get;
            set;
        }

        public int Eroded
        {
            get;
            set;
        }
        public override bool FightCallback => false;

        public LifeCharacteristic()
        {
        }

        public LifeCharacteristic(int @base) : base(@base)
        {
        }

        public override Characteristic Clone()
        {
            return new LifeCharacteristic()
            {
                Base = this.Base,
                Additional = this.Additional,
                Objects = this.Objects,
            };
        }
        public override CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            var value = TotalInContext();
            return new CharacterCharacteristicValue(value, (short)characteristic);

            return base.GetCharacterCharacteristic(characteristic);
        }

        public void Bind(DetailedCharacteristic vitality)
        {
            this.Vitality = vitality;
        }
        public static new LifeCharacteristic New(int @base)
        {
            return new LifeCharacteristic(@base);
        }

        public override int TotalInContext()
        {
            return base.TotalInContext() + this.Vitality.TotalInContext() - Eroded;
        }
   
    }
}
