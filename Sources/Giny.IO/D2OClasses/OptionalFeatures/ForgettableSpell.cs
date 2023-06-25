using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ForgettableSpell", "")]
    public class ForgettableSpell : IDataObject , IIndexedData
    {        public const string MODULE = "ForgettableSpells";

        public int Id => (int)id;

        public int id;
        public int pairId;
        public int itemId;

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
        public int PairId
        {
            get
            {
                return pairId;
            }
            set
            {
                pairId = value;
            }
        }
        [D2OIgnore]
        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
            }
        }

    }}
