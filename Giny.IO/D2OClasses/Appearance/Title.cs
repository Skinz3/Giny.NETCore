using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("Title", "")]
    public class Title : IDataObject, IIndexedData
    {
        public const string MODULE = "Titles";

        public int Id => (int)id;

        public int id;
        public uint nameMaleId;
        public uint nameFemaleId;
        public bool visible;
        public int categoryId;

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
        public uint NameMaleId
        {
            get
            {
                return nameMaleId;
            }
            set
            {
                nameMaleId = value;
            }
        }
        [D2OIgnore]
        public uint NameFemaleId
        {
            get
            {
                return nameFemaleId;
            }
            set
            {
                nameFemaleId = value;
            }
        }
        [D2OIgnore]
        public bool Visible
        {
            get
            {
                return visible;
            }
            set
            {
                visible = value;
            }
        }
        [D2OIgnore]
        public int CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
            }
        }

    }
}
