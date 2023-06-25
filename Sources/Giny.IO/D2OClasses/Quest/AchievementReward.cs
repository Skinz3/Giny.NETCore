using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AchievementReward", "")]
    public class AchievementReward : IDataObject , IIndexedData
    {        public const string MODULE = "AchievementRewards";

        public int Id => (int)id;

        public uint id;
        public uint achievementId;
        public string criteria;
        public double kamasRatio;
        public double experienceRatio;
        public bool kamasScaleWithPlayerLevel;
        public List<uint> itemsReward;
        public List<uint> itemsQuantityReward;
        public List<uint> emotesReward;
        public List<uint> spellsReward;
        public List<uint> titlesReward;
        public List<uint> ornamentsReward;

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
        public uint AchievementId
        {
            get
            {
                return achievementId;
            }
            set
            {
                achievementId = value;
            }
        }
        [D2OIgnore]
        public string Criteria
        {
            get
            {
                return criteria;
            }
            set
            {
                criteria = value;
            }
        }
        [D2OIgnore]
        public double KamasRatio
        {
            get
            {
                return kamasRatio;
            }
            set
            {
                kamasRatio = value;
            }
        }
        [D2OIgnore]
        public double ExperienceRatio
        {
            get
            {
                return experienceRatio;
            }
            set
            {
                experienceRatio = value;
            }
        }
        [D2OIgnore]
        public bool KamasScaleWithPlayerLevel
        {
            get
            {
                return kamasScaleWithPlayerLevel;
            }
            set
            {
                kamasScaleWithPlayerLevel = value;
            }
        }
        [D2OIgnore]
        public List<uint> ItemsReward
        {
            get
            {
                return itemsReward;
            }
            set
            {
                itemsReward = value;
            }
        }
        [D2OIgnore]
        public List<uint> ItemsQuantityReward
        {
            get
            {
                return itemsQuantityReward;
            }
            set
            {
                itemsQuantityReward = value;
            }
        }
        [D2OIgnore]
        public List<uint> EmotesReward
        {
            get
            {
                return emotesReward;
            }
            set
            {
                emotesReward = value;
            }
        }
        [D2OIgnore]
        public List<uint> SpellsReward
        {
            get
            {
                return spellsReward;
            }
            set
            {
                spellsReward = value;
            }
        }
        [D2OIgnore]
        public List<uint> TitlesReward
        {
            get
            {
                return titlesReward;
            }
            set
            {
                titlesReward = value;
            }
        }
        [D2OIgnore]
        public List<uint> OrnamentsReward
        {
            get
            {
                return ornamentsReward;
            }
            set
            {
                ornamentsReward = value;
            }
        }

    }}
