using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class PointDodgeCharacteristic : RelativeCharacteristic
    {
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

        public override short Total()
        {
            short total = base.Total();
            return (short)(total > 0 ? total : 0);
        }
        public override short TotalInContext()
        {
            short totalInContext = base.Total();
            return (short)(totalInContext > 0 ? totalInContext : 0);
        }
        public static new PointDodgeCharacteristic Zero()
        {
            return new PointDodgeCharacteristic();
        }
        public static new PointDodgeCharacteristic New(short delta)
        {
            return new PointDodgeCharacteristic()
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
            return new PointDodgeCharacteristic()
            {
                Base = base.Base,
                Additional = Additional,
                Objects = Objects,
                Relativ = Relativ,
            };
        }

    }
}
