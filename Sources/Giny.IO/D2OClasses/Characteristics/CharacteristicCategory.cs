using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CharacteristicCategory", "")]
    public class CharacteristicCategory : IDataObject , IIndexedData
    {        public const string MODULE = "CharacteristicCategories";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int order;
        public List<uint> characteristicIds;

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
        public List<uint> CharacteristicIds
        {
            get
            {
                return characteristicIds;
            }
            set
            {
                characteristicIds = value;
            }
        }

    }}
