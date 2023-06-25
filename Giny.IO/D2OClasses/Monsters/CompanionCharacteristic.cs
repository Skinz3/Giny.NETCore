using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CompanionCharacteristic", "")]
    public class CompanionCharacteristic : IDataObject , IIndexedData
    {        public const string MODULE = "CompanionCharacteristics";

        public int Id => (int)id;

        public int id;
        public int caracId;
        public int companionId;
        public int order;
        public List<List<double>> statPerLevelRange;

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
        public int CaracId
        {
            get
            {
                return caracId;
            }
            set
            {
                caracId = value;
            }
        }
        [D2OIgnore]
        public int CompanionId
        {
            get
            {
                return companionId;
            }
            set
            {
                companionId = value;
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
        public List<List<double>> StatPerLevelRange
        {
            get
            {
                return statPerLevelRange;
            }
            set
            {
                statPerLevelRange = value;
            }
        }

    }}
