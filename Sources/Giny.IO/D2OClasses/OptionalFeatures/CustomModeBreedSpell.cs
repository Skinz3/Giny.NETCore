using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("CustomModeBreedSpell", "")]
    public class CustomModeBreedSpell : IDataObject , IIndexedData
    {        public const string MODULE = "CustomModeBreedSpells";

        public int Id => (int)id;

        public int id;
        public int pairId;
        public int breedId;
        public bool isInitialSpell;
        public bool isHidden;

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
        public int BreedId
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
        public bool IsInitialSpell
        {
            get
            {
                return isInitialSpell;
            }
            set
            {
                isInitialSpell = value;
            }
        }
        [D2OIgnore]
        public bool IsHidden
        {
            get
            {
                return isHidden;
            }
            set
            {
                isHidden = value;
            }
        }

    }}
