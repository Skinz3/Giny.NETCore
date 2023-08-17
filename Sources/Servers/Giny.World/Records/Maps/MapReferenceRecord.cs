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
    [D2OClass("MapReference")]
    [Table("map_references")]
    public class MapReferenceRecord : IRecord
    {
        [Container]
        private static Dictionary<long, MapReferenceRecord> MapReferences = new Dictionary<long, MapReferenceRecord>();

        [D2OField("id")]
        [Primary]
        public long Id
        {
            get;
            set;
        }

        [D2OField("mapId")]
        public long MapId
        {
            get;
            set;
        }

        [D2OField("cellId")]
        public short CellId
        {
            get;
            set;
        }

        public static MapReferenceRecord? GetMapReference(long id)
        {
            if (MapReferences.ContainsKey(id))
            {
                return MapReferences[id];
            }
            else
            {
                return null;
            }
        }
    }
}
