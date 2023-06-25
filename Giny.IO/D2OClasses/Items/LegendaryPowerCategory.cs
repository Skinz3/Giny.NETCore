using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("LegendaryPowerCategory", "")]
    public class LegendaryPowerCategory : IDataObject , IIndexedData
    {        public const string MODULE = "LegendaryPowersCategories";

        public int Id => (int)id;

        public int id;
        public string categoryName;
        public bool categoryOverridable;
        public List<int> categorySpells;

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
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
            }
        }
        [D2OIgnore]
        public bool CategoryOverridable
        {
            get
            {
                return categoryOverridable;
            }
            set
            {
                categoryOverridable = value;
            }
        }
        [D2OIgnore]
        public List<int> CategorySpells
        {
            get
            {
                return categorySpells;
            }
            set
            {
                categorySpells = value;
            }
        }

    }}
