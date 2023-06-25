using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MapPosition", "")]
    public class MapPosition : IDataObject , IIndexedData
    {        public const string MODULE = "MapPositions";

        public int Id => throw new NotImplementedException();

        public double id;
        public int posX;
        public int posY;
        public bool outdoor;
        public int capabilities;
        public int nameId;
        public bool showNameOnFingerpost;
        public List<List<int>> playlists;
        public int subAreaId;
        public int worldMap;
        public bool hasPriorityOnWorldmap;
        public bool allowPrism;
        public bool isTransition;
        public bool mapHasTemplate;
        public uint tacticalModeTemplateId;
        public bool hasPublicPaddock;

        [D2OIgnore]
        public double Id_
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
        public int PosX
        {
            get
            {
                return posX;
            }
            set
            {
                posX = value;
            }
        }
        [D2OIgnore]
        public int PosY
        {
            get
            {
                return posY;
            }
            set
            {
                posY = value;
            }
        }
        [D2OIgnore]
        public bool Outdoor
        {
            get
            {
                return outdoor;
            }
            set
            {
                outdoor = value;
            }
        }
        [D2OIgnore]
        public int Capabilities
        {
            get
            {
                return capabilities;
            }
            set
            {
                capabilities = value;
            }
        }
        [D2OIgnore]
        public int NameId
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
        public bool ShowNameOnFingerpost
        {
            get
            {
                return showNameOnFingerpost;
            }
            set
            {
                showNameOnFingerpost = value;
            }
        }
        [D2OIgnore]
        public List<List<int>> Playlists
        {
            get
            {
                return playlists;
            }
            set
            {
                playlists = value;
            }
        }
        [D2OIgnore]
        public int SubAreaId
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
        public int WorldMap
        {
            get
            {
                return worldMap;
            }
            set
            {
                worldMap = value;
            }
        }
        [D2OIgnore]
        public bool HasPriorityOnWorldmap
        {
            get
            {
                return hasPriorityOnWorldmap;
            }
            set
            {
                hasPriorityOnWorldmap = value;
            }
        }
        [D2OIgnore]
        public bool AllowPrism
        {
            get
            {
                return allowPrism;
            }
            set
            {
                allowPrism = value;
            }
        }
        [D2OIgnore]
        public bool IsTransition
        {
            get
            {
                return isTransition;
            }
            set
            {
                isTransition = value;
            }
        }
        [D2OIgnore]
        public bool MapHasTemplate
        {
            get
            {
                return mapHasTemplate;
            }
            set
            {
                mapHasTemplate = value;
            }
        }
        [D2OIgnore]
        public uint TacticalModeTemplateId
        {
            get
            {
                return tacticalModeTemplateId;
            }
            set
            {
                tacticalModeTemplateId = value;
            }
        }
        [D2OIgnore]
        public bool HasPublicPaddock
        {
            get
            {
                return hasPublicPaddock;
            }
            set
            {
                hasPublicPaddock = value;
            }
        }

    }}
