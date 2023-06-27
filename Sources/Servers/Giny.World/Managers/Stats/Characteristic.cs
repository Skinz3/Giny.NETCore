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
    [ProtoInclude(4, typeof(ApCharacteristic))]
    [ProtoInclude(5, typeof(MpCharacteristic))]
    [ProtoInclude(6, typeof(PointDodgeCharacteristic))]
    [ProtoInclude(7, typeof(RangeCharacteristic))]
    [ProtoInclude(8, typeof(RelativeCharacteristic))]
    [ProtoInclude(9, typeof(ResistanceCharacteristic))]
    [ProtoContract]
    public class Characteristic
    {
        public event Action<Characteristic> OnContextChanged;

        [ProtoMember(1)]
        public virtual short Base
        {
            get;
            set;
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
        // ignore
        public virtual short Context
        {
            get
            {
                return m_context;
            }
            set
            {
                bool dirty = m_context != value;
                m_context = value;

                if (dirty)
                {
                    OnContextChanged?.Invoke(this);
                }
            }
        }

        private short m_context
        {
            get;
            set;
        }
        /// <summary>
        /// We dont clone context...?
        /// </summary>
        public virtual Characteristic Clone()
        {
            return new Characteristic()
            {
                Additional = Additional,
                Base = Base,
                Objects = Objects
            };
        }
        public static Characteristic Zero()
        {
            return New(0);
        }
        public static Characteristic New(short @base)
        {
            return new Characteristic()
            {
                Base = @base,
                Additional = 0,
                Context = 0,
                Objects = 0
            };
        }
        public virtual CharacterCharacteristicDetailed GetCharacterCharacteristicDetailed(CharacteristicEnum characteristic)
        {

            return new CharacterCharacteristicDetailed(Base, Additional, Objects, 0, Context, (short)characteristic);
        }
        public virtual short Total()
        {
            return (short)(Base + Additional + Objects);
        }
        public virtual short TotalInContext()
        {
            return (short)(Total() + Context);
        }
        public override string ToString()
        {
            return "Total Context : " + TotalInContext();
        }
    }
}
