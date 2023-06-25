using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Companion", "")]
    public class Companion : IDataObject , IIndexedData
    {        public const string MODULE = "Companions";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public string look;
        public bool webDisplay;
        public uint descriptionId;
        public uint startingSpellLevelId;
        public uint assetId;
        public List<uint> characteristics;
        public List<uint> spells;
        public int creatureBoneId;
        public string visibility;

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
        public string Look
        {
            get
            {
                return look;
            }
            set
            {
                look = value;
            }
        }
        [D2OIgnore]
        public bool WebDisplay
        {
            get
            {
                return webDisplay;
            }
            set
            {
                webDisplay = value;
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
        public uint StartingSpellLevelId
        {
            get
            {
                return startingSpellLevelId;
            }
            set
            {
                startingSpellLevelId = value;
            }
        }
        [D2OIgnore]
        public uint AssetId
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
        public List<uint> Characteristics
        {
            get
            {
                return characteristics;
            }
            set
            {
                characteristics = value;
            }
        }
        [D2OIgnore]
        public List<uint> Spells
        {
            get
            {
                return spells;
            }
            set
            {
                spells = value;
            }
        }
        [D2OIgnore]
        public int CreatureBoneId
        {
            get
            {
                return creatureBoneId;
            }
            set
            {
                creatureBoneId = value;
            }
        }
        [D2OIgnore]
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
            }
        }

    }}
