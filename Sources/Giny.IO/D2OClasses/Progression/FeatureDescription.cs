using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("FeatureDescription", "")]
    public class FeatureDescription : IDataObject , IIndexedData
    {        public const string MODULE = "FeatureDescriptions";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint descriptionId;
        public uint priority;
        public uint parentId;
        public List<int> children;
        public string criterion;

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
        public uint DescriptionId
        {
            get
            {
                return descriptionId;
            }
            set
            {
                descriptionId = value;
            }
        }
        [D2OIgnore]
        public uint Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
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
        [D2OIgnore]
        public List<int> Children
        {
            get
            {
                return children;
            }
            set
            {
                children = value;
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

    }}
