using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("Modster", "")]
    public class Modster : IDataObject, IIndexedData
    {
        public const string MODULE = "Modsters";

        public int Id => (int)id;

        public int id;
        public int itemId;
        public int modsterId;
        public int order;
        public List<int> parentsModsterId;
        public List<int> modsterActiveSpells;
        public List<int> modsterPassiveSpells;
        public List<int> modsterHiddenAchievements;
        public List<int> modsterAchievements;

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
        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
            }
        }
        [D2OIgnore]
        public int ModsterId
        {
            get
            {
                return modsterId;
            }
            set
            {
                modsterId = value;
            }
        }
        [D2OIgnore]
        public int Order
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
        public List<int> ParentsModsterId
        {
            get
            {
                return parentsModsterId;
            }
            set
            {
                parentsModsterId = value;
            }
        }
        [D2OIgnore]
        public List<int> ModsterActiveSpells
        {
            get
            {
                return modsterActiveSpells;
            }
            set
            {
                modsterActiveSpells = value;
            }
        }
        [D2OIgnore]
        public List<int> ModsterPassiveSpells
        {
            get
            {
                return modsterPassiveSpells;
            }
            set
            {
                modsterPassiveSpells = value;
            }
        }
        [D2OIgnore]
        public List<int> ModsterHiddenAchievements
        {
            get
            {
                return modsterHiddenAchievements;
            }
            set
            {
                modsterHiddenAchievements = value;
            }
        }
        [D2OIgnore]
        public List<int> ModsterAchievements
        {
            get
            {
                return modsterAchievements;
            }
            set
            {
                modsterAchievements = value;
            }
        }

    }
}

