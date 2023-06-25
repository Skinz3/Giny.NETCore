using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class RangeCharacteristic : LimitCharacteristic
    {
        public const short RangeLimit = 6;

        // ignore
        public override short Limit
        {
            get
            {
                return RangeLimit;
            }
        }

        // ignore
        public override bool ContextLimit
        {
            get
            {
                return true;
            }
        }

        [ProtoMember(1)]
        public override short Base { get => base.Base; set => base.Base = value; }

        [ProtoMember(2)]
        public override short Additional { get => base.Additional; set => base.Additional = value; }

        [ProtoMember(3)]
        public override short Objects { get => base.Objects; set => base.Objects = value; }

        public new static RangeCharacteristic New(short @base)
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
