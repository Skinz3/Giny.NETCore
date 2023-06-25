using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("WorldMap", "")]
    public class WorldMap : IDataObject , IIndexedData
    {        public const string MODULE = "WorldMaps";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int origineX;
        public int origineY;
        public double mapWidth;
        public double mapHeight;
        public bool viewableEverywhere;
        public double minScale;
        public double maxScale;
        public double startScale;
        public int totalWidth;
        public int totalHeight;
        public List<string> zoom;
        public bool visibleOnMap;

        [D2OIgnore]
        public int Id_
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
        public uint NameId
        {
            get
            {
                return nameId;
            }
            set
            {
                nameId = value;
            }
        }
        [D2OIgnore]
        public int OrigineX
        {
            get
            {
                return origineX;
            }
            set
            {
                origineX = value;
            }
        }
        [D2OIgnore]
        public int OrigineY
        {
            get
            {
                return origineY;
            }
            set
            {
                origineY = value;
            }
        }
        [D2OIgnore]
        public double MapWidth
        {
            get
            {
                return mapWidth;
            }
            set
            {
                mapWidth = value;
            }
        }
        [D2OIgnore]
        public double MapHeight
        {
            get
            {
                return mapHeight;
            }
            set
            {
                mapHeight = value;
            }
        }
        [D2OIgnore]
        public bool ViewableEverywhere
        {
            get
            {
                return viewableEverywhere;
            }
            set
            {
                viewableEverywhere = value;
            }
        }
        [D2OIgnore]
        public double MinScale
        {
            get
            {
                return minScale;
            }
            set
            {
                minScale = value;
            }
        }
        [D2OIgnore]
        public double MaxScale
        {
            get
            {
                return maxScale;
            }
            set
            {
                maxScale = value;
            }
        }
        [D2OIgnore]
        public double StartScale
        {
            get
            {
                return startScale;
            }
            set
            {
                startScale = value;
            }
        }
        [D2OIgnore]
        public int TotalWidth
        {
            get
            {
                return totalWidth;
            }
            set
            {
                totalWidth = value;
            }
        }
        [D2OIgnore]
        public int TotalHeight
        {
            get
            {
                return totalHeight;
            }
            set
            {
                totalHeight = value;
            }
        }
        [D2OIgnore]
        public List<string> Zoom
        {
            get
            {
                return zoom;
            }
            set
            {
                zoom = value;
            }
        }
        [D2OIgnore]
        public bool VisibleOnMap
        {
            get
            {
                return visibleOnMap;
            }
            set
            {
                visibleOnMap = value;
            }
        }

    }}
