using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("QuestStep", "")]
    public class QuestStep : IDataObject , IIndexedData
    {        public const string MODULE = "QuestSteps";

        public int Id => (int)id;

        public uint id;
        public uint questId;
        public uint nameId;
        public uint descriptionId;
        public int dialogId;
        public uint optimalLevel;
        public double duration;
        public List<uint> objectiveIds;
        public List<uint> rewardsIds;

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
        public uint QuestId
        {
            get
            {
                return questId;
            }
            set
            {
                questId = value;
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
        public int DialogId
        {
            get
            {
                return dialogId;
            }
            set
            {
                dialogId = value;
            }
        }
        [D2OIgnore]
        public uint OptimalLevel
        {
            get
            {
                return optimalLevel;
            }
            set
            {
                optimalLevel = value;
            }
        }
        [D2OIgnore]
        public double Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }
        [D2OIgnore]
        public List<uint> ObjectiveIds
        {
            get
            {
                return objectiveIds;
            }
            set
            {
                objectiveIds = value;
            }
        }
        [D2OIgnore]
        public List<uint> RewardsIds
        {
            get
            {
                return rewardsIds;
            }
            set
            {
                rewardsIds = value;
            }
        }

    }}
