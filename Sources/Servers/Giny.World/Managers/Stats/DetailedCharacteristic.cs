using Giny.Protocol.Custom.Enums;
using Giny.Protocol.Types;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Stats
{
    [ProtoContract]
    [ProtoInclude(4, typeof(ApCharacteristic))]
    [ProtoInclude(5, typeof(MpCharacteristic))]
    [ProtoInclude(6, typeof(PointDodgeCharacteristic))]
    [ProtoInclude(7, typeof(RangeCharacteristic))]
    [ProtoInclude(8, typeof(RelativeCharacteristic))]
    [ProtoInclude(9, typeof(ResistanceCharacteristic))]
    [ProtoInclude(10, typeof(ErosionCharacteristic))]
    public class DetailedCharacteristic : Characteristic
    {
        public virtual short? Limit => null;

        public virtual bool ContextualLimit => false;

        [ProtoMember(1)]
        public override short Base
        {
            get
            {
                return base.Base;
            }
            set
            {
                base.Base = value;
            }
        }
        [ProtoMember(2)]
        public virtual short Additional
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public virtual short Objects
        {
            get;
            set;
        }

        /// <summary>
        /// We dont clone context...?
        /// </summary>
        public override Characteristic Clone()
        {
            return new DetailedCharacteristic()
            {
                Additional = Additional,
                Base = Base,
                Objects = Objects
            };
        }
        public static new DetailedCharacteristic Zero()
        {
            return New(0);
        }
        public static new DetailedCharacteristic New(short @base)
        {
            return new DetailedCharacteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }
        public override CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterCharacteristicDetailed(Base, Additional, Objects, 0, Context, (short)characteristic);
        }
        public override short Total()
        {
            var total = (short)(Base + Additional + Objects);

            if (!Limit.HasValue)
            {
                return total;
            }
            return total > Limit.Value ? Limit.Value : total;
        }
        public override short TotalInContext()
        {
            var totalContext = (short)(Total() + Context);

            if (ContextualLimit && Limit.HasValue)
            {
                return totalContext > Limit.Value ? Limit.Value : totalContext;
            }
            else
            {
                return totalContext;
            }
        }
        public override string ToString()
        {
            return "Total Context : " + TotalInContext();
        }
    }
}
