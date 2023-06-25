using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("QuestStepRewards", "")]
    public class QuestStepRewards : IDataObject , IIndexedData
    {        public const string MODULE = "QuestStepRewards";

        public int Id => (int)id;

        public uint id;
        public uint stepId;
        public int levelMin;
        public int levelMax;
        public double kamasRatio;
        public double experienceRatio;
        public bool kamasScaleWithPlayerLevel;
        public List<List<uint>> itemsReward;
        public List<uint> emotesReward;
        public List<uint> jobsReward;
        public List<uint> spellsReward;
        public List<uint> titlesReward;

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
        public uint StepId
        {
            get
            {
                return stepId;
            }
            set
            {
                stepId = value;
            }
        }
        [D2OIgnore]
        public int LevelMin
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
        public int LevelMax
        {
            get
            {
                return levelMax;
            }
            set
            {
                levelMax = value;
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
        public List<List<uint>> ItemsReward
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
        public List<uint> JobsReward
        {
            get
            {
                return jobsReward;
            }
            set
            {
                jobsReward = value;
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

    }}
