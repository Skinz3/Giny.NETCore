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

        public override int Total()
        {
            int total = base.Total();
            return (short)(total > 0 ? total : 0);
        }
        public override int TotalInContext()
        {
            int totalInContext = base.Total();
            return  totalInContext > 0 ? totalInContext : 0;
        }
        public static new PointDodgeCharacteristic Zero()
        {
            return new PointDodgeCharacteristic();
        }
        public static new PointDodgeCharacteristic New(int delta)
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
