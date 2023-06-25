using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Skill", "")]
    public class Skill : IDataObject , IIndexedData
    {        public const string MODULE = "Skills";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int parentJobId;
        public bool isForgemagus;
        public List<int> modifiableItemTypeIds;
        public int gatheredRessourceItem;
        public List<int> craftableItemIds;
        public int interactiveId;
        public int range;
        public bool useRangeInClient;
        public string useAnimation;
        public int cursor;
        public int elementActionId;
        public bool availableInHouse;
        public uint levelMin;
        public bool clientDisplay;

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
        public int ParentJobId
        {
            get
            {
                return parentJobId;
            }
            set
            {
                parentJobId = value;
            }
        }
        [D2OIgnore]
        public bool IsForgemagus
        {
            get
            {
                return isForgemagus;
            }
            set
            {
                isForgemagus = value;
            }
        }
        [D2OIgnore]
        public List<int> ModifiableItemTypeIds
        {
            get
            {
                return modifiableItemTypeIds;
            }
            set
            {
                modifiableItemTypeIds = value;
            }
        }
        [D2OIgnore]
        public int GatheredRessourceItem
        {
            get
            {
                return gatheredRessourceItem;
            }
            set
            {
                gatheredRessourceItem = value;
            }
        }
        [D2OIgnore]
        public List<int> CraftableItemIds
        {
            get
            {
                return craftableItemIds;
            }
            set
            {
                craftableItemIds = value;
            }
        }
        [D2OIgnore]
        public int InteractiveId
        {
            get
            {
                return interactiveId;
            }
            set
            {
                interactiveId = value;
            }
        }
        [D2OIgnore]
        public int Range
        {
            get
            {
                return range;
            }
            set
            {
                range = value;
            }
        }
        [D2OIgnore]
        public bool UseRangeInClient
        {
            get
            {
                return useRangeInClient;
            }
            set
            {
                useRangeInClient = value;
            }
        }
        [D2OIgnore]
        public string UseAnimation
        {
            get
            {
                return useAnimation;
            }
            set
            {
                useAnimation = value;
            }
        }
        [D2OIgnore]
        public int Cursor
        {
            get
            {
                return cursor;
            }
            set
            {
                cursor = value;
            }
        }
        [D2OIgnore]
        public int ElementActionId
        {
            get
            {
                return elementActionId;
            }
            set
            {
                elementActionId = value;
            }
        }
        [D2OIgnore]
        public bool AvailableInHouse
        {
            get
            {
                return availableInHouse;
            }
            set
            {
                availableInHouse = value;
            }
        }
        [D2OIgnore]
        public uint LevelMin
        {
            get
            {
                return levelMin;
            }
            set
            {
                levelMin = value;
            }
        }
        [D2OIgnore]
        public bool ClientDisplay
        {
            get
            {
                return clientDisplay;
            }
            set
            {
                clientDisplay = value;
            }
        }

    }}
