using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EvolutiveItemType", "")]
    public class EvolutiveItemType : IDataObject , IIndexedData
    {        public const string MODULE = "EvolutiveItemTypes";

        public int Id => (int)id;

        public int id;
        public uint maxLevel;
        public double experienceBoost;
        public List<int> experienceByLevel;

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
        public uint MaxLevel
        {
            get
            {
                return maxLevel;
            }
            set
            {
                maxLevel = value;
            }
        }
        [D2OIgnore]
        public double ExperienceBoost
        {
            get
            {
                return experienceBoost;
            }
            set
            {
                experienceBoost = value;
            }
        }
        [D2OIgnore]
        public List<int> ExperienceByLevel
        {
            get
            {
                return experienceByLevel;
            }
            set
            {
                experienceByLevel = value;
            }
        }

    }}
