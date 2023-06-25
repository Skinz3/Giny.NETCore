using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("House", "")]
    public class House : IDataObject , IIndexedData
    {        public const string MODULE = "Houses";

        public int Id => throw new NotImplementedException();

        public int typeId;
        public uint defaultPrice;
        public int nameId;
        public int descriptionId;
        public int gfxId;

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
        public uint DefaultPrice
        {
            get
            {
                return defaultPrice;
            }
            set
            {
                defaultPrice = value;
            }
        }
        [D2OIgnore]
        public int NameId
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
        public int DescriptionId
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
        public int GfxId
        {
            get
            {
                return gfxId;
            }
            set
            {
                gfxId = value;
            }
        }

    }}
