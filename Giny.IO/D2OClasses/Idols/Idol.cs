using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Idol", "")]
    public class Idol : IDataObject , IIndexedData
    {        public const string MODULE = "Idols";

        public int Id => (int)id;

        public int id;
        public string description;
        public int categoryId;
        public int itemId;
        public bool groupOnly;
        public int spellPairId;
        public int score;
        public int experienceBonus;
        public int dropBonus;
        public List<int> synergyIdolsIds;
        public List<double> synergyIdolsCoeff;
        public List<int> incompatibleMonsters;

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
        public bool GroupOnly
        {
            get
            {
                return groupOnly;
            }
            set
            {
                groupOnly = value;
            }
        }
        [D2OIgnore]
        public int SpellPairId
        {
            get
            {
                return spellPairId;
            }
            set
            {
                spellPairId = value;
            }
        }
        [D2OIgnore]
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        [D2OIgnore]
        public int ExperienceBonus
        {
            get
            {
                return experienceBonus;
            }
            set
            {
                experienceBonus = value;
            }
        }
        [D2OIgnore]
        public int DropBonus
        {
            get
            {
                return dropBonus;
            }
            set
            {
                dropBonus = value;
            }
        }
        [D2OIgnore]
        public List<int> SynergyIdolsIds
        {
            get
            {
                return synergyIdolsIds;
            }
            set
            {
                synergyIdolsIds = value;
            }
        }
        [D2OIgnore]
        public List<double> SynergyIdolsCoeff
        {
            get
            {
                return synergyIdolsCoeff;
            }
            set
            {
                synergyIdolsCoeff = value;
            }
        }
        [D2OIgnore]
        public List<int> IncompatibleMonsters
        {
            get
            {
                return incompatibleMonsters;
            }
            set
            {
                incompatibleMonsters = value;
            }
        }

    }}
