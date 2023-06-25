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
    [D2OClass("Title")]
    [Table("titles")]
    public class TitleRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, TitleRecord> Titles = new ConcurrentDictionary<long, TitleRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [D2OField("nameMaleId")]
        [I18NField]
        public string Name
        {
            get;
            set;
        }

        public static bool Exists(short id)
        {
            return Titles.ContainsKey(id);
        }
    }
}
