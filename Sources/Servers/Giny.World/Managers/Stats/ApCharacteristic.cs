using Giny.Core.IO.Configuration;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    public class ApCharacteristic : UsableCharacteristic
    {
        public ApCharacteristic(int @base) : base(@base)
        {

        }

        public ApCharacteristic()
        {

        }

        public override int? Limit => ConfigManager<WorldConfig>.Instance.ApLimit;


        [ProtoMember(1)]
        public override int Base { get => base.Base; set => base.Base = value; }

        [ProtoMember(2)]
        public override int Additional { get => base.Additional; set => base.Additional = value; }

        public override int Objects { get => base.Objects; set => base.Objects = value; }

        public static new ApCharacteristic New(int @base)
        {
            return new ApCharacteristic(@base)
            {
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }
        public override Characteristic Clone()
        {
            return new ApCharacteristic(Base)
            {
                Additional = Additional,
                Objects = Objects
            };
        }

    }
}
