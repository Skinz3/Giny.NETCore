using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Waypoint", "")]
    public class Waypoint : IDataObject , IIndexedData
    {        public const string MODULE = "Waypoints";

        public int Id => (int)id;

        public uint id;
        public double mapId;
        public uint subAreaId;
        public bool activated;

        [D2OIgnore]
        public uint Id_
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        [D2OIgnore]
        public double MapId
        {
            get
            {
                return mapId;
            }
            set
            {
                mapId = value;
            }
        }
        [D2OIgnore]
        public uint SubAreaId
        {
            get
            {
                return subAreaId;
            }
            set
            {
                subAreaId = value;
            }
        }
        [D2OIgnore]
        public bool Activated
        {
            get
            {
                return activated;
            }
            set
            {
                activated = value;
            }
        }

    }}
