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
    [ProtoInclude(13, typeof(LifeLossCharacteristic))]
    public class Characteristic
    {
        public event Action<Characteristic> OnContextChanged;

        [ProtoMember(1)]
        public virtual int Base
        {
            get;
            set;
        }


        public Characteristic()
        {

        }

        public Characteristic(int @base)
        {
            this.Base = @base;
        }
        /// <summary>
        /// When value change, automatically notify its value 
        /// to client in fights.
        /// </summary>
        public virtual bool FightCallback
        {
            get
            {
                return true;
            }
        }
        // ignore
        public virtual int Context
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

        private int m_context
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
        public static Characteristic New(int @base)
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
        public virtual int Total()
        {
            return Base;
        }
        public virtual int TotalInContext()
        {
            return (short)(Total() + Context);
        }
        public override string ToString()
        {
            return "Total Context : " + TotalInContext();
        }
    }
}
