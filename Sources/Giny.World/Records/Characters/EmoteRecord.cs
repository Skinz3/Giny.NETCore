using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Characters
{
    [D2OClass("Emoticon")]
    [Table("emotes")]
    public class EmoteRecord : ITable
    {
        [Container]
        private static readonly ConcurrentDictionary<long, EmoteRecord> Emotes = new ConcurrentDictionary<long, EmoteRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
        {
            get;
            set;
        }
        [I18NField]
        [D2OField("nameId")]
        public string Name
        {
            get;
            set;
        }
        [D2OField("aura")]
        public bool IsAura
        {
            get;
            set;
        }
        public static EmoteRecord GetEmote(short id)
        {
            return Emotes[id];
        }
    }
}
