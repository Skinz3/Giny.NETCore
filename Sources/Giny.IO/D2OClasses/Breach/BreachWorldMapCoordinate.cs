using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("BreachWorldMapCoordinate", "")]
    public class BreachWorldMapCoordinate : IDataObject , IIndexedData
    {        public const string MODULE = "BreachWorldMapCoordinates";

        public int Id => (int)id;

        public uint id;
        public uint mapStage;
        public int mapCoordinateX;
        public int mapCoordinateY;
        public int unexploredMapIcon;
        public int exploredMapIcon;

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
        public uint MapStage
        {
            get
            {
                return mapStage;
            }
            set
            {
                mapStage = value;
            }
        }
        [D2OIgnore]
        public int MapCoordinateX
        {
            get
            {
                return mapCoordinateX;
            }
            set
            {
                mapCoordinateX = value;
            }
        }
        [D2OIgnore]
        public int MapCoordinateY
        {
            get
            {
                return mapCoordinateY;
            }
            set
            {
                mapCoordinateY = value;
            }
        }
        [D2OIgnore]
        public int UnexploredMapIcon
        {
            get
            {
                return unexploredMapIcon;
            }
            set
            {
                unexploredMapIcon = value;
            }
        }
        [D2OIgnore]
        public int ExploredMapIcon
        {
            get
            {
                return exploredMapIcon;
            }
            set
            {
                exploredMapIcon = value;
            }
        }

    }}
