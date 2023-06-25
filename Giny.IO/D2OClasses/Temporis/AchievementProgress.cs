using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AchievementProgress", "")]
    public class AchievementProgress : IDataObject , IIndexedData
    {        public const string MODULE = "AchievementProgress";

        public int Id => (int)id;

        public int id;
        public string name;
        public int seasonId;

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
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        [D2OIgnore]
        public int SeasonId
        {
            get
            {
                return seasonId;
            }
            set
            {
                seasonId = value;
            }
        }

    }}
