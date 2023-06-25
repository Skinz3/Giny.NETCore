using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("EffectInstanceMount", "")]
    public class EffectInstanceMount : EffectInstance , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public double id;
        public double expirationDate;
        public uint model;
        public string name;
        public string owner;
        public uint level;
        public bool sex;
        public bool isRideable;
        public bool isFeconded;
        public bool isFecondationReady;
        public int reproductionCount;
        public uint reproductionCountMax;
        public List<EffectInstanceInteger> effects;
        public List<uint> capacities;

        [D2OIgnore]
        public double Id_
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
        public double ExpirationDate
        {
            get
            {
                return expirationDate;
            }
            set
            {
                expirationDate = value;
            }
        }
        [D2OIgnore]
        public uint Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
        [D2OIgnore]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        [D2OIgnore]
        public string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }
        [D2OIgnore]
        public uint Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }
        [D2OIgnore]
        public bool Sex
        {
            get
            {
                return sex;
            }
            set
            {
                sex = value;
            }
        }
        [D2OIgnore]
        public bool IsRideable
        {
            get
            {
                return isRideable;
            }
            set
            {
                isRideable = value;
            }
        }
        [D2OIgnore]
        public bool IsFeconded
        {
            get
            {
                return isFeconded;
            }
            set
            {
                isFeconded = value;
            }
        }
        [D2OIgnore]
        public bool IsFecondationReady
        {
            get
            {
                return isFecondationReady;
            }
            set
            {
                isFecondationReady = value;
            }
        }
        [D2OIgnore]
        public int ReproductionCount
        {
            get
            {
                return reproductionCount;
            }
            set
            {
                reproductionCount = value;
            }
        }
        [D2OIgnore]
        public uint ReproductionCountMax
        {
            get
            {
                return reproductionCountMax;
            }
            set
            {
                reproductionCountMax = value;
            }
        }
        [D2OIgnore]
        public List<EffectInstanceInteger> Effects
        {
            get
            {
                return effects;
            }
            set
            {
                effects = value;
            }
        }
        [D2OIgnore]
        public List<uint> Capacities
        {
            get
            {
                return capacities;
            }
            set
            {
                capacities = value;
            }
        }

    }}
