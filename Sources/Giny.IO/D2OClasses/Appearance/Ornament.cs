using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Ornament", "")]
    public class Ornament : IDataObject , IIndexedData
    {        public const string MODULE = "Ornaments";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public bool visible;
        public int assetId;
        public int iconId;
        public int order;

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
        public int AssetId
        {
            get
            {
                return assetId;
            }
            set
            {
                assetId = value;
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

    }}
