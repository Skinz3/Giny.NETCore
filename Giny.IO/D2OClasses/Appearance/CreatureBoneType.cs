using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CreatureBoneType", "")]
    public class CreatureBoneType : IDataObject , IIndexedData
    {        public const string MODULE = "CreatureBonesTypes";

        public int Id => (int)id;

        public int id;
        public int creatureBoneId;

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

    }}
