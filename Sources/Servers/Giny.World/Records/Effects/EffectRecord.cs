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
    public class EffectRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, EffectRecord> Effects = new ConcurrentDictionary<long, EffectRecord>();

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

        public static EffectRecord GetEffectRecord(EffectsEnum effectEnum)
        {
            return Effects[(long)effectEnum];
        }
    }
}
