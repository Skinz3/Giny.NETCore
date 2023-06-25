using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AchievementProgressStep", "")]
    public class AchievementProgressStep : IDataObject , IIndexedData
    {        public const string MODULE = "AchievementProgressSteps";

        public int Id => (int)id;

        public int id;
        public int progressId;
        public int score;
        public bool isCosmetic;
        public int achievementId;

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
        public int ProgressId
        {
            get
            {
                return progressId;
            }
            set
            {
                progressId = value;
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
        public bool IsCosmetic
        {
            get
            {
                return isCosmetic;
            }
            set
            {
                isCosmetic = value;
            }
        }
        [D2OIgnore]
        public int AchievementId
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

    }}
