using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("TaxCollectorFirstname", "")]
    public class TaxCollectorFirstname : IDataObject , IIndexedData
    {        public const string MODULE = "TaxCollectorFirstnames";

        public int Id => (int)id;

        public int id;
        public uint firstnameId;

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
        public uint FirstnameId
        {
            get
            {
                return firstnameId;
            }
            set
            {
                firstnameId = value;
            }
        }

    }}
