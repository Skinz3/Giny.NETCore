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
    public class DetailedCharacteristic : Characteristic
    {
        public event Action<Characteristic> OnContextChanged;

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
            return (short)(Base + Additional + Objects);
        }
        public override short TotalInContext()
        {
            return (short)(Total() + Context);
        }
        public override string ToString()
        {
            return "Total Context : " + TotalInContext();
        }
    }
}
