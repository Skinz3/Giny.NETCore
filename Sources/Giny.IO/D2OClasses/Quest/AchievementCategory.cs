using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AchievementCategory", "")]
    public class AchievementCategory : IDataObject , IIndexedData
    {        public const string MODULE = "AchievementCategories";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint parentId;
        public string icon;
        public uint order;
        public string color;
        public List<uint> achievementIds;
        public string visibilityCriterion;

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
        public uint ParentId
        {
            get
            {
                return parentId;
            }
            set
            {
                parentId = value;
            }
        }
        [D2OIgnore]
        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
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
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        [D2OIgnore]
        public List<uint> AchievementIds
        {
            get
            {
                return achievementIds;
            }
            set
            {
                achievementIds = value;
            }
        }
        [D2OIgnore]
        public string VisibilityCriterion
        {
            get
            {
                return visibilityCriterion;
            }
            set
            {
                visibilityCriterion = value;
            }
        }

    }}
