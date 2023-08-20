using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class ResistanceCharacteristic : DetailedCharacteristic
    {
        public const int ResistanceLimit = 50;

        public override int? Limit => ResistanceLimit;

        public override bool ContextualLimit => true;
      

        [ProtoMember(1)]
        public override int Base { get => base.Base; set => base.Base = value; }

        [ProtoMember(2)]
        public override int Additional { get => base.Additional; set => base.Additional = value; }

        public override int Objects { get => base.Objects; set => base.Objects = value; }

        public static new ResistanceCharacteristic New(short @base)
        {
            return new ResistanceCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }

        public new static ResistanceCharacteristic Zero()
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
