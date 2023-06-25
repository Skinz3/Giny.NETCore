using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AchievementObjective", "")]
    public class AchievementObjective : IDataObject , IIndexedData
    {        public const string MODULE = "AchievementObjectives";

        public int Id => (int)id;

        public uint id;
        public uint achievementId;
        public uint order;
        public uint nameId;
        public string criterion;

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
        public uint Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
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
        public string Criterion
        {
            get
            {
                return criterion;
            }
            set
            {
                criterion = value;
            }
        }

    }}
