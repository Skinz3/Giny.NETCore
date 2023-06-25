using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using Giny.World.Managers.Fights.Effects.Buffs;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class RelativeCharacteristic : Characteristic
    {
        protected Characteristic Relativ
        {
            get;
            set;
        }
        public short RelativDelta
        {
            get
            {
                return (short)(Relativ.Total() / 10);
            }
        }
        [ProtoMember(1)]
        public override short Base
        {
            get => base.Base;
            set => base.Base = value;
        }
        [ProtoMember(2)]
        public override short Additional
        {
            get => base.Additional;
            set => base.Additional = value;
        }
        [ProtoMember(3)]
        public override short Objects
        {
            get => base.Objects;
            set => base.Objects = value;
        }

        public RelativeCharacteristic()
        {

        }
        public void Bind(Characteristic relative)
        {
            this.Relativ = relative;
        }
        public override CharacterCharacteristicDetailed GetCharacterCharacteristicDetailed(CharacteristicEnum characteristic)
        {
            return new CharacterCharacteristicDetailed((short)(Base + RelativDelta), Additional, Objects, Context, Context, (short)characteristic);
        }
        public static new RelativeCharacteristic Zero()
        {
            return new RelativeCharacteristic();
        }
        public static new RelativeCharacteristic New(short delta)
        {
            return new RelativeCharacteristic()
            {
                Base = delta,
                Additional = 0,
                Context = 0,
                Objects = 0,
                Relativ = null
            };
        }
        public override Characteristic Clone()
        {
            return new RelativeCharacteristic()
            {
                Base = base.Base,
                Additional = Additional,
                Objects = Objects,
                Relativ = Relativ,
            };
        }
        public override short Total()
        {
            return (short)(RelativDelta + Base + Additional + Objects);
        }
    }
}
