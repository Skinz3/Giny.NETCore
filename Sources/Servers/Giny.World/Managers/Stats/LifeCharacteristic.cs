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

        public int Loss
        {
            get;
            set;
        }

        public int Current
        {
            get
            {
                return (TotalInContext() - Loss) + Eroded;
            }
            set
            {
                Loss = TotalInContext() - value + Eroded;
            }
        }

        public double Percentage => Current / (double)TotalInContext() * 100d;

        public int TotalWithoutErosion => base.TotalInContext() + Vitality.TotalInContext();

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

        public CharacterCharacteristic GetHitpointLossCharactersitic()
        {
            return new CharacterCharacteristicValue(-Loss, (short)CharacteristicEnum.HIT_POINT_LOSS);
        }
        public override CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterCharacteristicValue(TotalWithoutErosion, (short)characteristic);
        }

        public void Initialize(DetailedCharacteristic vitality)
        {
            this.Vitality = vitality;
        }
        public static new LifeCharacteristic New(int @base)
        {
            return new LifeCharacteristic(@base);
        }

        public override int Total()
        {
            return base.Total() + Vitality.Total();
        }
        public override int TotalInContext()
        {
            return base.TotalInContext() + Vitality.TotalInContext() - Eroded;
        }

    }
}
