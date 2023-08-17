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
    [ProtoInclude(3, typeof(DetailedCharacteristic))]
    public class Characteristic
    {
        public event Action<Characteristic> OnContextChanged;

        [ProtoMember(1)]
        public virtual short Base
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
                    this.OnContextChanged?.Invoke(this);
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
                Base = Base,
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

                Context = 0,

            };
        }


        public virtual CharacterCharacteristic GetCharacterCharacteristic(CharacteristicEnum characteristic)
        {
            return new CharacterCharacteristicValue(TotalInContext(), (short)characteristic);
        }
        public virtual short Total()
        {
            return Base;
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
