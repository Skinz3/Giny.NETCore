using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Head", "")]
    public class Head : IDataObject , IIndexedData
    {        public const string MODULE = "Heads";

        public int Id => (int)id;

        public int id;
        public string skins;
        public string assetId;
        public uint breed;
        public uint gender;
        public string label;
        public uint order;

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
        public string Skins
        {
            get
            {
                return skins;
            }
            set
            {
                skins = value;
            }
        }
        [D2OIgnore]
        public string AssetId
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
        public uint Breed
        {
            get
            {
                return breed;
            }
            set
            {
                breed = value;
            }
        }
        [D2OIgnore]
        public uint Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }
        [D2OIgnore]
        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }
        [D2OIgnore]
        public uint Order
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
