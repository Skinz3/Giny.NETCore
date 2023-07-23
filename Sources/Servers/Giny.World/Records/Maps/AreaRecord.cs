using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Maps
{
    [D2OClass("Area")]
    [Table("areas")]
    public class AreaRecord : IRecord
    {
        [Container]
        private static ConcurrentDictionary<long, AreaRecord> Areas = new ConcurrentDictionary<long, AreaRecord>();

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
        [D2OField("superAreaId")]
        public int SuperAreaId
        {
            get;
            set;
        }
        [D2OField("worldmapId")]
        public int WorldMapId
        {
            get;
            set;
        }

        public static AreaRecord GetArea(long id)
        {
            return Areas[id];
        }
    }
}
