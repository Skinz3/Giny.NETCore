using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("RandomDropGroup", "")]
    public class RandomDropGroup : IDataObject , IIndexedData
    {        public const string MODULE = "RandomDropGroups";

        public int Id => (int)id;

        public uint id;
        public string name;
        public string description;
        public List<RandomDropItem> randomDropItems;
        public bool displayContent;
        public bool displayChances;

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
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        [D2OIgnore]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        [D2OIgnore]
        public List<RandomDropItem> RandomDropItems
        {
            get
            {
                return randomDropItems;
            }
            set
            {
                randomDropItems = value;
            }
        }
        [D2OIgnore]
        public bool DisplayContent
        {
            get
            {
                return displayContent;
            }
            set
            {
                displayContent = value;
            }
        }
        [D2OIgnore]
        public bool DisplayChances
        {
            get
            {
                return displayChances;
            }
            set
            {
                displayChances = value;
            }
        }

    }}
