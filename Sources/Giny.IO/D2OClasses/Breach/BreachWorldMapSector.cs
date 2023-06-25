using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("BreachWorldMapSector", "")]
    public class BreachWorldMapSector : IDataObject , IIndexedData
    {        public const string MODULE = "BreachWorldMapSectors";

        public int Id => (int)id;

        public uint id;
        public uint sectorNameId;
        public uint legendId;
        public string sectorIcon;
        public int minStage;
        public int maxStage;

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
        public uint SectorNameId
        {
            get
            {
                return sectorNameId;
            }
            set
            {
                sectorNameId = value;
            }
        }
        [D2OIgnore]
        public uint LegendId
        {
            get
            {
                return legendId;
            }
            set
            {
                legendId = value;
            }
        }
        [D2OIgnore]
        public string SectorIcon
        {
            get
            {
                return sectorIcon;
            }
            set
            {
                sectorIcon = value;
            }
        }
        [D2OIgnore]
        public int MinStage
        {
            get
            {
                return minStage;
            }
            set
            {
                minStage = value;
            }
        }
        [D2OIgnore]
        public int MaxStage
        {
            get
            {
                return maxStage;
            }
            set
            {
                maxStage = value;
            }
        }

    }}
