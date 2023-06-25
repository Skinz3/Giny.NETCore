using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Effect", "")]
    public class Effect : IDataObject , IIndexedData
    {        public const string MODULE = "Effects";

        public int Id => (int)id;

        public int id;
        public uint descriptionId;
        public uint iconId;
        public int characteristic;
        public uint category;
        public string @operator;
        public bool showInTooltip;
        public bool useDice;
        public bool forceMinMax;
        public bool boost;
        public bool active;
        public int oppositeId;
        public uint theoreticalDescriptionId;
        public uint theoreticalPattern;
        public bool showInSet;
        public int bonusType;
        public bool useInFight;
        public uint effectPriority;
        public double effectPowerRate;
        public int elementId;

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
        public int Characteristic
        {
            get
            {
                return characteristic;
            }
            set
            {
                characteristic = value;
            }
        }
        [D2OIgnore]
        public uint Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }
        [D2OIgnore]
        public string Operator
        {
            get
            {
                return @operator;
            }
            set
            {
                @operator = value;
            }
        }
        [D2OIgnore]
        public bool ShowInTooltip
        {
            get
            {
                return showInTooltip;
            }
            set
            {
                showInTooltip = value;
            }
        }
        [D2OIgnore]
        public bool UseDice
        {
            get
            {
                return useDice;
            }
            set
            {
                useDice = value;
            }
        }
        [D2OIgnore]
        public bool ForceMinMax
        {
            get
            {
                return forceMinMax;
            }
            set
            {
                forceMinMax = value;
            }
        }
        [D2OIgnore]
        public bool Boost
        {
            get
            {
                return boost;
            }
            set
            {
                boost = value;
            }
        }
        [D2OIgnore]
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        [D2OIgnore]
        public int OppositeId
        {
            get
            {
                return oppositeId;
            }
            set
            {
                oppositeId = value;
            }
        }
        [D2OIgnore]
        public uint TheoreticalDescriptionId
        {
            get
            {
                return theoreticalDescriptionId;
            }
            set
            {
                theoreticalDescriptionId = value;
            }
        }
        [D2OIgnore]
        public uint TheoreticalPattern
        {
            get
            {
                return theoreticalPattern;
            }
            set
            {
                theoreticalPattern = value;
            }
        }
        [D2OIgnore]
        public bool ShowInSet
        {
            get
            {
                return showInSet;
            }
            set
            {
                showInSet = value;
            }
        }
        [D2OIgnore]
        public int BonusType
        {
            get
            {
                return bonusType;
            }
            set
            {
                bonusType = value;
            }
        }
        [D2OIgnore]
        public bool UseInFight
        {
            get
            {
                return useInFight;
            }
            set
            {
                useInFight = value;
            }
        }
        [D2OIgnore]
        public uint EffectPriority
        {
            get
            {
                return effectPriority;
            }
            set
            {
                effectPriority = value;
            }
        }
        [D2OIgnore]
        public double EffectPowerRate
        {
            get
            {
                return effectPowerRate;
            }
            set
            {
                effectPowerRate = value;
            }
        }
        [D2OIgnore]
        public int ElementId
        {
            get
            {
                return elementId;
            }
            set
            {
                elementId = value;
            }
        }

    }}
