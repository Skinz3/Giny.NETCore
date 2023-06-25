using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SubArea", "")]
    public class SubArea : IDataObject , IIndexedData
    {        public const string MODULE = "SubAreas";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int areaId;
        public List<List<int>> playlists;
        public List<double> mapIds;
        public Rectangle bounds;
        public List<int> shape;
        public int worldmapId;
        public List<uint> customWorldMap;
        public int packId;
        public uint level;
        public bool isConquestVillage;
        public bool basicAccountAllowed;
        public bool displayOnWorldMap;
        public bool mountAutoTripAllowed;
        public bool psiAllowed;
        public List<uint> monsters;
        public bool capturable;
        public List<List<double>> quests;
        public List<List<double>> npcs;
        public List<int> harvestables;
        public int associatedZaapMapId;

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
        public int AreaId
        {
            get
            {
                return areaId;
            }
            set
            {
                areaId = value;
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
        public List<double> MapIds
        {
            get
            {
                return mapIds;
            }
            set
            {
                mapIds = value;
            }
        }
        [D2OIgnore]
        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
            set
            {
                bounds = value;
            }
        }
        [D2OIgnore]
        public List<int> Shape
        {
            get
            {
                return shape;
            }
            set
            {
                shape = value;
            }
        }
        [D2OIgnore]
        public int WorldmapId
        {
            get
            {
                return worldmapId;
            }
            set
            {
                worldmapId = value;
            }
        }
        [D2OIgnore]
        public List<uint> CustomWorldMap
        {
            get
            {
                return customWorldMap;
            }
            set
            {
                customWorldMap = value;
            }
        }
        [D2OIgnore]
        public int PackId
        {
            get
            {
                return packId;
            }
            set
            {
                packId = value;
            }
        }
        [D2OIgnore]
        public uint Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        [D2OIgnore]
        public bool IsConquestVillage
        {
            get
            {
                return isConquestVillage;
            }
            set
            {
                isConquestVillage = value;
            }
        }
        [D2OIgnore]
        public bool BasicAccountAllowed
        {
            get
            {
                return basicAccountAllowed;
            }
            set
            {
                basicAccountAllowed = value;
            }
        }
        [D2OIgnore]
        public bool DisplayOnWorldMap
        {
            get
            {
                return displayOnWorldMap;
            }
            set
            {
                displayOnWorldMap = value;
            }
        }
        [D2OIgnore]
        public bool MountAutoTripAllowed
        {
            get
            {
                return mountAutoTripAllowed;
            }
            set
            {
                mountAutoTripAllowed = value;
            }
        }
        [D2OIgnore]
        public bool PsiAllowed
        {
            get
            {
                return psiAllowed;
            }
            set
            {
                psiAllowed = value;
            }
        }
        [D2OIgnore]
        public List<uint> Monsters
        {
            get
            {
                return monsters;
            }
            set
            {
                monsters = value;
            }
        }
        [D2OIgnore]
        public bool Capturable
        {
            get
            {
                return capturable;
            }
            set
            {
                capturable = value;
            }
        }
        [D2OIgnore]
        public List<List<double>> Quests
        {
            get
            {
                return quests;
            }
            set
            {
                quests = value;
            }
        }
        [D2OIgnore]
        public List<List<double>> Npcs
        {
            get
            {
                return npcs;
            }
            set
            {
                npcs = value;
            }
        }
        [D2OIgnore]
        public List<int> Harvestables
        {
            get
            {
                return harvestables;
            }
            set
            {
                harvestables = value;
            }
        }
        [D2OIgnore]
        public int AssociatedZaapMapId
        {
            get
            {
                return associatedZaapMapId;
            }
            set
            {
                associatedZaapMapId = value;
            }
        }

    }}
