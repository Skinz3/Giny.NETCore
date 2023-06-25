using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("BreedRole", "")]
    public class BreedRole : IDataObject , IIndexedData
    {        public const string MODULE = "BreedRoles";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint descriptionId;
        public int assetId;
        public int color;

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
        public int Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

    }}
