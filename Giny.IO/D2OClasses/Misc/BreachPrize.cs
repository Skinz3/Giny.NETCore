using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("BreachPrize", "")]
    public class BreachPrize : IDataObject , IIndexedData
    {        public const string MODULE = "BreachPrizes";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int currency;
        public string tooltipKey;
        public uint descriptionKey;
        public int categoryId;
        public int itemId;

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
        public int Currency
        {
            get
            {
                return currency;
            }
            set
            {
                currency = value;
            }
        }
        [D2OIgnore]
        public string TooltipKey
        {
            get
            {
                return tooltipKey;
            }
            set
            {
                tooltipKey = value;
            }
        }
        [D2OIgnore]
        public uint DescriptionKey
        {
            get
            {
                return descriptionKey;
            }
            set
            {
                descriptionKey = value;
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
        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
            }
        }

    }}
