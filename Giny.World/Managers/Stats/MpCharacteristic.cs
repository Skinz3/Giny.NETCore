using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class MpCharacteristic : LimitCharacteristic
    {
        public override short Limit
        {
            get
            {
                return ConfigFile.Instance.MpLimit;
            }
        }

        public override bool ContextLimit
        {
            get
            {
                return false;
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


        public new static MpCharacteristic New(short @base)
        {
            return new MpCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }
        public override Characteristic Clone()
        {
            return new MpCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Objects = Objects
            };
        }
    }
}
