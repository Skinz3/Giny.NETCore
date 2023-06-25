using Giny.Core;
using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using Giny.Protocol.Custom.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Maps
{
    [D2OClass("MapScrollAction")]
    [Table("mapscrollactions")]
    public class MapScrollActionRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, MapScrollActionRecord> MapScrollActions = new ConcurrentDictionary<long, MapScrollActionRecord>();

        [Ignore]
        public long Id => MapId;

        [D2OField("id")]
        [Primary]
        public int MapId
        {
            get;
            set;
        }
        [D2OField("rightMapId")]
        public int RightMapId
        {
            get;
            set;
        }
        [D2OField("leftMapId")]
        public int LeftMapId
        {
            get;
            set;
        }
        [D2OField("topMapId")]
        public int TopMapId
        {
            get;
            set;
        }
        [D2OField("bottomMapId")]
        public int BottomMapId
        {
            get;
            set;
        }
        public static MapScrollActionRecord GetMapScrollAction(long mapId)
        {
            return MapScrollActions.ContainsKey(mapId) ? MapScrollActions[mapId] : null;
        }

    }
}
