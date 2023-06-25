using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MonsterMiniBoss", "")]
    public class MonsterMiniBoss : IDataObject , IIndexedData
    {        public const string MODULE = "MonsterMiniBoss";

        public int Id => (int)id;

        public int id;
        public int monsterReplacingId;

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
        public int MonsterReplacingId
        {
            get
            {
                return monsterReplacingId;
            }
            set
            {
                monsterReplacingId = value;
            }
        }

    }}
