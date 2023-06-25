using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CharacterXPMapping", "")]
    public class CharacterXPMapping : IDataObject , IIndexedData
    {        public const string MODULE = "CharacterXPMappings";

        public int Id => throw new NotImplementedException();

        public int level;
        public double experiencePoints;

        [D2OIgnore]
        public int Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        [D2OIgnore]
        public double ExperiencePoints
        {
            get
            {
                return experiencePoints;
            }
            set
            {
                experiencePoints = value;
            }
        }

    }}
