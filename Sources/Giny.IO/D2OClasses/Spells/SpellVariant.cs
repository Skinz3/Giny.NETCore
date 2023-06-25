using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SpellVariant", "")]
    public class SpellVariant : IDataObject , IIndexedData
    {        public const string MODULE = "SpellVariants";

        public int Id => (int)id;

        public int id;
        public uint breedId;
        public List<uint> spellIds;

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
        public uint BreedId
        {
            get
            {
                return breedId;
            }
            set
            {
                breedId = value;
            }
        }
        [D2OIgnore]
        public List<uint> SpellIds
        {
            get
            {
                return spellIds;
            }
            set
            {
                spellIds = value;
            }
        }

    }}
