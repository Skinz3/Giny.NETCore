using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Quest", "")]
    public class Quest : IDataObject , IIndexedData
    {        public const string MODULE = "Quests";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public List<uint> stepIds;
        public uint categoryId;
        public uint repeatType;
        public uint repeatLimit;
        public bool isDungeonQuest;
        public uint levelMin;
        public uint levelMax;
        public bool isPartyQuest;
        public string startCriterion;
        public bool followable;

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
        public List<uint> StepIds
        {
            get
            {
                return stepIds;
            }
            set
            {
                stepIds = value;
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
        public uint RepeatType
        {
            get
            {
                return repeatType;
            }
            set
            {
                repeatType = value;
            }
        }
        [D2OIgnore]
        public uint RepeatLimit
        {
            get
            {
                return repeatLimit;
            }
            set
            {
                repeatLimit = value;
            }
        }
        [D2OIgnore]
        public bool IsDungeonQuest
        {
            get
            {
                return isDungeonQuest;
            }
            set
            {
                isDungeonQuest = value;
            }
        }
        [D2OIgnore]
        public uint LevelMin
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
        public uint LevelMax
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
        public bool IsPartyQuest
        {
            get
            {
                return isPartyQuest;
            }
            set
            {
                isPartyQuest = value;
            }
        }
        [D2OIgnore]
        public string StartCriterion
        {
            get
            {
                return startCriterion;
            }
            set
            {
                startCriterion = value;
            }
        }
        [D2OIgnore]
        public bool Followable
        {
            get
            {
                return followable;
            }
            set
            {
                followable = value;
            }
        }

    }}
