using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class ErosionCharacteristic : DetailedCharacteristic
    {
        public const short ErosionLimit = 50;

        public override short? Limit => ErosionLimit;

        public override bool ContextualLimit => true;


        [ProtoMember(1)]
        public override short Base { get => base.Base; set => base.Base = value; }

        [ProtoMember(2)]
        public override short Additional { get => base.Additional; set => base.Additional = value; }

        [ProtoMember(3)]
        public override short Objects { get => base.Objects; set => base.Objects = value; }

        public static new ErosionCharacteristic New(short @base)
        {
            return new ErosionCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }

        public new static ErosionCharacteristic Zero()
        {
            return New(0);
        }

        public override Characteristic Clone()
        {
            return new ResistanceCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Objects = Objects
            };
        }
    }
}
