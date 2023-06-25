using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SpellConversion", "")]
    public class SpellConversion : IDataObject , IIndexedData
    {        public const string MODULE = "SpellConversions";

        public int Id => throw new NotImplementedException();

        public uint oldSpellId;
        public uint newSpellId;

        [D2OIgnore]
        public uint OldSpellId
        {
            get
            {
                return oldSpellId;
            }
            set
            {
                oldSpellId = value;
            }
        }
        [D2OIgnore]
        public uint NewSpellId
        {
            get
            {
                return newSpellId;
            }
            set
            {
                newSpellId = value;
            }
        }

    }}
