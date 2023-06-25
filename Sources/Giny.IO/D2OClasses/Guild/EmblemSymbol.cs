using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EmblemSymbol", "")]
    public class EmblemSymbol : IDataObject , IIndexedData
    {        public const string MODULE = "EmblemSymbols";

        public int Id => (int)id;

        public int id;
        public int iconId;
        public int skinId;
        public int order;
        public int categoryId;
        public bool colorizable;

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
        public int IconId
        {
            get
            {
                return iconId;
            }
            set
            {
                iconId = value;
            }
        }
        [D2OIgnore]
        public int SkinId
        {
            get
            {
                return skinId;
            }
            set
            {
                skinId = value;
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
        public bool Colorizable
        {
            get
            {
                return colorizable;
            }
            set
            {
                colorizable = value;
            }
        }

    }}
