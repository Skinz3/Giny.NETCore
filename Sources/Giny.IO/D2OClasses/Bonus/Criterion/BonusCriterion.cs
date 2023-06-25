using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("BonusCriterion", "")]
    public class BonusCriterion : IDataObject , IIndexedData
    {        public const string MODULE = "BonusesCriterions";

        public int Id => (int)id;

        public int id;
        public uint type;
        public int value;

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
        public uint Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        [D2OIgnore]
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                value = value;
            }
        }

    }}
