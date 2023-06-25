using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("StealthBones", "")]
    public class StealthBones : IDataObject , IIndexedData
    {        public const string MODULE = "StealthBones";

        public int Id => (int)id;

        public uint id;

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

    }}
