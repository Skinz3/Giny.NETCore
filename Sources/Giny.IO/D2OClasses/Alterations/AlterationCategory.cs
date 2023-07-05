using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("AlterationCategory", "")]
    public class AlterationCategory : IDataObject, IIndexedData
    {
        public const string MODULE = "AlterationCategories";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint parentId;

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
        public uint ParentId
        {
            get
            {
                return parentId;
            }
            set
            {
                parentId = value;
            }
        }

    }
}

