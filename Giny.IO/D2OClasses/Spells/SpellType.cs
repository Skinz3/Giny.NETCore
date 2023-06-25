using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SpellType", "")]
    public class SpellType : IDataObject , IIndexedData
    {        public const string MODULE = "SpellTypes";

        public int Id => (int)id;

        public int id;
        public uint longNameId;
        public uint shortNameId;

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
        public uint LongNameId
        {
            get
            {
                return longNameId;
            }
            set
            {
                longNameId = value;
            }
        }
        [D2OIgnore]
        public uint ShortNameId
        {
            get
            {
                return shortNameId;
            }
            set
            {
                shortNameId = value;
            }
        }

    }}
