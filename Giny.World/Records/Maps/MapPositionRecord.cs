using Giny.IO.D2O;
using Giny.ORM.Attributes;
using Giny.ORM.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Maps
{
    [D2OClass("MapPosition")]
    [Table("mappositions")]
    public class MapPositionRecord : ITable
    {
        [Container]
        private static ConcurrentDictionary<long, MapPositionRecord> MapPositions = new ConcurrentDictionary<long, MapPositionRecord>();

        [Primary]
        [D2OField("id")]
        public long Id
        {
            get;
            set;
        }
        [D2OField("posX")]
        public int X
        {
            get;
            set;
        }
        [D2OField("posY")]
        public int Y
        {
            get;
            set;
        }
        [D2OField("outdoor")]
        public bool Outdoor
        {
            get;
            set;
        }
        [D2OField("capabilities")]
        public int Capabilities
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
        [Ignore]
        public Point Point
        {
            get
            {
                return new Point(X, Y);
            }
        }



        [Ignore]
        public bool AllowChallenge
        {
            get
            {
                return (this.Capabilities & 1) != 0;
            }
        }
        [Ignore]
        public bool AllowAggression
        {
            get
            {
                return (this.Capabilities & 2) != 0;
            }
        }
        [Ignore]
        public bool AllowTeleportTo
        {
            get
            {
                return (this.Capabilities & 4) != 0;
            }
        }
        [Ignore]
        public bool AllowTeleportFrom
        {
            get
            {
                return (this.Capabilities & 8) != 0;
            }
        }
        [Ignore]
        public bool AllowExchangesBetweenPlayers
        {
            get
            {
                return (this.Capabilities & 16) != 0;
            }
        }
        [Ignore]
        public bool AllowHumanVendor
        {
            get
            {
                return (this.Capabilities & 32) != 0;
            }
        }
        [Ignore]
        public bool AllowCollector
        {
            get
            {
                return (this.Capabilities & 64) != 0;
            }
        }
        [Ignore]
        public bool AllowSoulCapture
        {
            get
            {
                return (this.Capabilities & 128) != 0;
            }
        }
        [Ignore]
        public bool AllowSoulSummon
        {
            get
            {
                return (this.Capabilities & 256) != 0;
            }
        }
        [Ignore]
        public bool AllowTavernRegen
        {
            get
            {
                return (this.Capabilities & 512) != 0;
            }
        }
        [Ignore]
        public bool AllowTombMode
        {
            get
            {
                return (this.Capabilities & 1024) != 0;
            }
        }
        [Ignore]
        public bool AllowTeleportEverywhere
        {
            get
            {
                return (this.Capabilities & 2048) != 0;
            }
        }
        [Ignore]
        public bool AllowFightChallenges
        {
            get
            {
                return (this.Capabilities & 4096) != 0;
            }
        }
        [Ignore]
        public bool AllowMonsterRespawn
        {
            get
            {
                return (this.Capabilities & 8192) != 0;
            }
        }
        [Ignore]
        public bool AllowMonsterFight
        {
            get
            {
                return (this.Capabilities & 16384) != 0;
            }
        }
        [Ignore]
        public bool AllowMount
        {
            get
            {
                return (this.Capabilities & 32768) != 0;
            }
        }
        [Ignore]
        public bool AllowObjectDisposal
        {
            get
            {
                return (this.Capabilities & 65536) != 0;
            }
        }
        [Ignore]
        public bool AllowUnderwater
        {
            get
            {
                return (this.Capabilities & 131072) != 0;
            }
        }
        [Ignore]
        public bool AllowPVP1VS1
        {
            get
            {
                return (this.Capabilities & 262144) != 0;
            }
        }
        [Ignore]
        public bool AllowPVP3VS3
        {
            get
            {
                return (this.Capabilities & 524288) != 0;
            }
        }
        [Ignore]
        public bool AllowMonsterAgression
        {
            get
            {
                return (this.Capabilities & 1048576) != 0;
            }
        }
        public static MapPositionRecord GetMapPosition(long mapId)
        {
            MapPositionRecord result = null;
            MapPositions.TryGetValue(mapId, out result);
            return result;
        }
        public static IEnumerable<MapPositionRecord> GetMapPositions()
        {
            return MapPositions.Values;
        }

    }
}
