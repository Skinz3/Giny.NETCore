using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ActivitySuggestion", "")]
    public class ActivitySuggestion : IDataObject , IIndexedData
    {        public const string MODULE = "ActivitySuggestions";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint descriptionId;
        public int categoryId;
        public uint level;
        public double mapId;
        public bool isLarge;
        public double startDate;
        public double endDate;
        public string icon;

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
        public int CategoryId
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
        public bool IsLarge
        {
            get
            {
                return isLarge;
            }
            set
            {
                isLarge = value;
            }
        }
        [D2OIgnore]
        public double StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }
        [D2OIgnore]
        public double EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
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

    }}
