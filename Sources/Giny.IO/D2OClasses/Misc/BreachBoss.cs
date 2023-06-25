using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("BreachBoss", "")]
    public class BreachBoss : IDataObject , IIndexedData
    {        public const string MODULE = "BreachBosses";

        public int Id => (int)id;

        public int id;
        public int monsterId;
        public int category;
        public string apparitionCriterion;
        public string accessCriterion;
        public List<int> incompatibleBosses;
        public uint rewardId;

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
        public int MonsterId
        {
            get
            {
                return monsterId;
            }
            set
            {
                monsterId = value;
            }
        }
        [D2OIgnore]
        public int Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
        [D2OIgnore]
        public string ApparitionCriterion
        {
            get
            {
                return apparitionCriterion;
            }
            set
            {
                apparitionCriterion = value;
            }
        }
        [D2OIgnore]
        public string AccessCriterion
        {
            get
            {
                return accessCriterion;
            }
            set
            {
                accessCriterion = value;
            }
        }
        [D2OIgnore]
        public List<int> IncompatibleBosses
        {
            get
            {
                return incompatibleBosses;
            }
            set
            {
                incompatibleBosses = value;
            }
        }
        [D2OIgnore]
        public uint RewardId
        {
            get
            {
                return rewardId;
            }
            set
            {
                rewardId = value;
            }
        }

    }}
