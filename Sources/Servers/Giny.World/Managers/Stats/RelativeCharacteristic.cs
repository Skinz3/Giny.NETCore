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
    public class RelativeCharacteristic : DetailedCharacteristic
    {
        protected Characteristic Relativ
        {
            get;
            set;
        }
        public int RelativDelta
        {
            get
            {
                return (int)(Relativ.Total() / 10d);
            }
        }
        [ProtoMember(1)]
        public override int Base
        {
            get => base.Base;
            set => base.Base = value;
        }
        [ProtoMember(2)]
        public override int Additional
        {
            get => base.Additional;
            set => base.Additional = value;
        }
        public override int Objects
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
        public override CharacterCharacteristicDetailed GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterCharacteristicDetailed(Base + RelativDelta, Additional, Objects, Context, Context, (short)characteristic);
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
        public override int Total()
        {
            return RelativDelta + Base + Additional + Objects;
        }
        public override int TotalInContext()
        {
            var totalContext = RelativDelta + Base + Additional + ObjectsWithLimit + Context;

            if (ContextualLimit && Limit.HasValue)
            {
                return totalContext > Limit.Value ? Limit.Value : totalContext;
            }
            else
            {
                return totalContext;
            }
        }
    }
}
