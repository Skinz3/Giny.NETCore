using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SkinMapping", "")]
    public class SkinMapping : IDataObject , IIndexedData
    {        public const string MODULE = "SkinMappings";

        public int Id => (int)id;

        public int id;
        public int lowDefId;

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
        public int LowDefId
        {
            get
            {
                return lowDefId;
            }
            set
            {
                lowDefId = value;
            }
        }

    }}
