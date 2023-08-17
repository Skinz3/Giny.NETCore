using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("Collection", "")]
    public class Collection : IDataObject, IIndexedData
    {
        public const string MODULE = "Collections";

        public int Id => throw new NotImplementedException();

        public int typeId;
        public string name;
        public string criterion;
        public List<Collectable> collectables;

        [D2OIgnore]
        public int TypeId
        {
            get
            {
                return typeId;
            }
            set
            {
                typeId = value;
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
        [D2OIgnore]
        public List<Collectable> Collectables
        {
            get
            {
                return collectables;
            }
            set
            {
                collectables = value;
            }
        }

    }
}

