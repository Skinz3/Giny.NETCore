using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Characteristic", "")]
    public class Characteristic : IDataObject , IIndexedData
    {        public const string MODULE = "Characteristics";

        public int Id => (int)id;

        public int id;
        public string keyword;
        public uint nameId;
        public string asset;
        public int categoryId;
        public bool visible;
        public int order;
        public int scaleFormulaId;
        public bool upgradable;

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
        public string Keyword
        {
            get
            {
                return keyword;
            }
            set
            {
                keyword = value;
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
        public string Asset
        {
            get
            {
                return asset;
            }
            set
            {
                asset = value;
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
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }
        [D2OIgnore]
        public int ScaleFormulaId
        {
            get
            {
                return scaleFormulaId;
            }
            set
            {
                scaleFormulaId = value;
            }
        }
        [D2OIgnore]
        public bool Upgradable
        {
            get
            {
                return upgradable;
            }
            set
            {
                upgradable = value;
            }
        }

    }}
