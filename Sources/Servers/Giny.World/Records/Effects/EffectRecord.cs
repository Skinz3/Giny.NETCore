using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Effects
{
    [D2OClass("Effect")]
    [Table("effects")]
    public class EffectRecord : IRecord
    {
        [Container]
        private static readonly Dictionary<long, EffectRecord> Effects = new Dictionary<long, EffectRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
        {
            get;
            set;
        }
        [I18NField]
        [D2OField("descriptionId")]
        public string Description
        {
            get;
            set;
        }

        [D2OField("effectPriority")]
        public int Priority
        {
            get;
            set;
        }

        [D2OField("characteristic")]
        public int CharacteristicId
        {
            get;
            set;
        }

        [D2OField("useDice")]
        public bool UseDice
        {
            get;
            set;
        }

        [D2OField("forceMinMax")]
        public bool ForceMinMax
        {
            get;
            set;
        }
        [D2OField("boost")]
        public bool Boost
        {
            get;
            set;
        }

        [D2OField("active")]
        public bool Active
        {
            get;
            set;
        }


        [D2OField("oppositeId")]
        public bool OppositeId
        {
            get;
            set;
        }

        [D2OField("effectPowerRate")]
        public double EffectPowerRate
        {
            get;
            set;
        }

        [D2OField("elementId")]
        public short ElementId
        {
            get;
            set;
        }


 
        public static EffectRecord GetEffectRecord(EffectsEnum effectEnum)
        {
            return Effects[(long)effectEnum];
        }
    }
}
