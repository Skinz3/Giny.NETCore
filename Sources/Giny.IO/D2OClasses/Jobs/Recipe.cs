using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Recipe", "")]
    public class Recipe : IDataObject , IIndexedData
    {        public const string MODULE = "Recipes";

        public int Id => throw new NotImplementedException();

        public int resultId;
        public uint resultNameId;
        public uint resultTypeId;
        public uint resultLevel;
        public List<int> ingredientIds;
        public List<uint> quantities;
        public int jobId;
        public int skillId;
        public string changeVersion;
        public double tooltipExpirationDate;

        [D2OIgnore]
        public int ResultId
        {
            get
            {
                return resultId;
            }
            set
            {
                resultId = value;
            }
        }
        [D2OIgnore]
        public uint ResultNameId
        {
            get
            {
                return resultNameId;
            }
            set
            {
                resultNameId = value;
            }
        }
        [D2OIgnore]
        public uint ResultTypeId
        {
            get
            {
                return resultTypeId;
            }
            set
            {
                resultTypeId = value;
            }
        }
        [D2OIgnore]
        public uint ResultLevel
        {
            get
            {
                return resultLevel;
            }
            set
            {
                resultLevel = value;
            }
        }
        [D2OIgnore]
        public List<int> IngredientIds
        {
            get
            {
                return ingredientIds;
            }
            set
            {
                ingredientIds = value;
            }
        }
        [D2OIgnore]
        public List<uint> Quantities
        {
            get
            {
                return quantities;
            }
            set
            {
                quantities = value;
            }
        }
        [D2OIgnore]
        public int JobId
        {
            get
            {
                return jobId;
            }
            set
            {
                jobId = value;
            }
        }
        [D2OIgnore]
        public int SkillId
        {
            get
            {
                return skillId;
            }
            set
            {
                skillId = value;
            }
        }
        [D2OIgnore]
        public string ChangeVersion
        {
            get
            {
                return changeVersion;
            }
            set
            {
                changeVersion = value;
            }
        }
        [D2OIgnore]
        public double TooltipExpirationDate
        {
            get
            {
                return tooltipExpirationDate;
            }
            set
            {
                tooltipExpirationDate = value;
            }
        }

    }}
