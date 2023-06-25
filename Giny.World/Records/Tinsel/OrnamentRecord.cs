using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Tinsel
{
    [D2OClass("Ornament")]
    [Table("ornaments")]
    public class OrnamentRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, OrnamentRecord> Ornaments = new ConcurrentDictionary<long, OrnamentRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [D2OField("nameId")]
        [I18NField]
        public string Name
        {
            get;
            set;
        }

        public static bool Exists(short id)
        {
            return Ornaments.ContainsKey(id);
        }
    }
}
