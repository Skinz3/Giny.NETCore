using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EvolutiveEffect", "")]
    public class EvolutiveEffect : IDataObject , IIndexedData
    {        public const string MODULE = "EvolutiveEffects";

        public int Id => (int)id;

        public int id;
        public int actionId;
        public int targetId;
        public List<List<double>> progressionPerLevelRange;

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
        public int ActionId
        {
            get
            {
                return actionId;
            }
            set
            {
                actionId = value;
            }
        }
        [D2OIgnore]
        public int TargetId
        {
            get
            {
                return targetId;
            }
            set
            {
                targetId = value;
            }
        }
        [D2OIgnore]
        public List<List<double>> ProgressionPerLevelRange
        {
            get
            {
                return progressionPerLevelRange;
            }
            set
            {
                progressionPerLevelRange = value;
            }
        }

    }}
