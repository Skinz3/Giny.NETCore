using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Bonus", "")]
    public class Bonus : IDataObject , IIndexedData
    {        public const string MODULE = "Bonuses";

        public int Id => (int)id;

        public int id;
        public uint type;
        public int amount;
        public List<int> criterionsIds;

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
        public uint Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        [D2OIgnore]
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
            }
        }
        [D2OIgnore]
        public List<int> CriterionsIds
        {
            get
            {
                return criterionsIds;
            }
            set
            {
                criterionsIds = value;
            }
        }

    }}
