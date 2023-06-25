using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Achievement", "")]
    public class Achievement : IDataObject , IIndexedData
    {        public const string MODULE = "Achievements";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint categoryId;
        public uint descriptionId;
        public uint iconId;
        public uint points;
        public uint level;
        public uint order;
        public bool accountLinked;
        public List<int> objectiveIds;
        public List<int> rewardIds;

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
        public uint CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
            }
        }
        [D2OIgnore]
        public uint DescriptionId
        {
            get
            {
                return descriptionId;
            }
            set
            {
                descriptionId = value;
            }
        }
        [D2OIgnore]
        public uint IconId
        {
            get
            {
                return iconId;
            }
            set
            {
                iconId = value;
            }
        }
        [D2OIgnore]
        public uint Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
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
        public uint Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }
        [D2OIgnore]
        public bool AccountLinked
        {
            get
            {
                return accountLinked;
            }
            set
            {
                accountLinked = value;
            }
        }
        [D2OIgnore]
        public List<int> ObjectiveIds
        {
            get
            {
                return objectiveIds;
            }
            set
            {
                objectiveIds = value;
            }
        }
        [D2OIgnore]
        public List<int> RewardIds
        {
            get
            {
                return rewardIds;
            }
            set
            {
                rewardIds = value;
            }
        }

    }}
