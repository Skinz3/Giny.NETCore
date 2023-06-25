using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("IncarnationLevel", "")]
    public class IncarnationLevel : IDataObject , IIndexedData
    {        public const string MODULE = "IncarnationLevels";

        public int Id => (int)id;

        public int id;
        public int incarnationId;
        public int level;
        public uint requiredXp;

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
        public int IncarnationId
        {
            get
            {
                return incarnationId;
            }
            set
            {
                incarnationId = value;
            }
        }
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
        public uint RequiredXp
        {
            get
            {
                return requiredXp;
            }
            set
            {
                requiredXp = value;
            }
        }

    }}
