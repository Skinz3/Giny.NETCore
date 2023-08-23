using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class RangeCharacteristic : DetailedCharacteristic
    {
        public const int RangeLimit = 6;

        public override int? Limit => RangeLimit;

        [ProtoMember(1)]
        public override int Base { get => base.Base; set => base.Base = value; }

        [ProtoMember(2)]
        public override int Additional { get => base.Additional; set => base.Additional = value; }

        public override int Objects { get => base.Objects; set => base.Objects = value; }

        public new static RangeCharacteristic New(int @base)
        {
            return new RangeCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }

        public new static RangeCharacteristic Zero()
        {
            return New(0);
        }

        public override Characteristic Clone()
        {
            return new RangeCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Objects = Objects
            };
        }
    }
}
