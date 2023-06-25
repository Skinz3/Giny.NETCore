using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ActivitySuggestionsCategory", "")]
    public class ActivitySuggestionsCategory : IDataObject , IIndexedData
    {        public const string MODULE = "ActivitySuggestionsCategories";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint parentId;

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

    }}
