using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Pack", "")]
    public class Pack : IDataObject , IIndexedData
    {        public const string MODULE = "Pack";

        public int Id => (int)id;

        public int id;
        public string name;
        public bool hasSubAreas;

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
        public bool HasSubAreas
        {
            get
            {
                return hasSubAreas;
            }
            set
            {
                hasSubAreas = value;
            }
        }

    }}
