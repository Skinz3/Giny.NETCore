using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ItemType", "")]
    public class ItemType : IDataObject , IIndexedData
    {        public const string MODULE = "ItemTypes";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public uint superTypeId;
        public uint categoryId;
        public bool isInEncyclopedia;
        public bool plural;
        public uint gender;
        public string rawZone;
        public bool mimickable;
        public int craftXpRatio;
        public int evolutiveTypeId;

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
        public uint SuperTypeId
        {
            get
            {
                return superTypeId;
            }
            set
            {
                superTypeId = value;
            }
        }
        [D2OIgnore]
        public uint CategoryId
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
        public bool IsInEncyclopedia
        {
            get
            {
                return isInEncyclopedia;
            }
            set
            {
                isInEncyclopedia = value;
            }
        }
        [D2OIgnore]
        public bool Plural
        {
            get
            {
                return plural;
            }
            set
            {
                plural = value;
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
        public string RawZone
        {
            get
            {
                return rawZone;
            }
            set
            {
                rawZone = value;
            }
        }
        [D2OIgnore]
        public bool Mimickable
        {
            get
            {
                return mimickable;
            }
            set
            {
                mimickable = value;
            }
        }
        [D2OIgnore]
        public int CraftXpRatio
        {
            get
            {
                return craftXpRatio;
            }
            set
            {
                craftXpRatio = value;
            }
        }
        [D2OIgnore]
        public int EvolutiveTypeId
        {
            get
            {
                return evolutiveTypeId;
            }
            set
            {
                evolutiveTypeId = value;
            }
        }

    }}
