using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("Alteration", "")]
    public class Alteration : IDataObject, IIndexedData
    {
        public const string MODULE = "Alterations";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint descriptionId;
        public uint categoryId;
        public uint iconId;
        public bool isVisible;
        public string criteria;
        public bool isWebDisplay;
        public List<EffectInstance> possibleEffects;

        [D2OIgnore]
        public uint Id_
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
        public uint NameId
        {
            get
            {
                return nameId;
            }
            set
            {
                nameId = value;
            }
        }
        [D2OIgnore]
        public uint DescriptionId
        {
            get
            {
                return descriptionId;
            }
            set
            {
                descriptionId = value;
            }
        }
        [D2OIgnore]
        public uint CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
            }
        }
        [D2OIgnore]
        public uint IconId
        {
            get
            {
                return iconId;
            }
            set
            {
                iconId = value;
            }
        }
        [D2OIgnore]
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
            }
        }
        [D2OIgnore]
        public string Criteria
        {
            get
            {
                return criteria;
            }
            set
            {
                criteria = value;
            }
        }
        [D2OIgnore]
        public bool IsWebDisplay
        {
            get
            {
                return isWebDisplay;
            }
            set
            {
                isWebDisplay = value;
            }
        }
        [D2OIgnore]
        public List<EffectInstance> PossibleEffects
        {
            get
            {
                return possibleEffects;
            }
            set
            {
                possibleEffects = value;
            }
        }

    }
}

