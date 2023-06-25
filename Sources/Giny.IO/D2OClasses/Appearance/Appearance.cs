using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Appearance", "")]
    public class Appearance : IDataObject , IIndexedData
    {        public const string MODULE = "Appearances";

        public int Id => (int)id;

        public uint id;
        public uint type;
        public string data;

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
        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

    }}
