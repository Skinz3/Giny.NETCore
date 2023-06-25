using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ItemSet", "")]
    public class ItemSet : IDataObject , IIndexedData
    {        public const string MODULE = "ItemSets";

        public int Id => (int)id;

        public uint id;
        public List<uint> items;
        public uint nameId;
        public List<List<EffectInstance>> effects;
        public bool bonusIsSecret;

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
        public List<uint> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
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
        public List<List<EffectInstance>> Effects
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
        public bool BonusIsSecret
        {
            get
            {
                return bonusIsSecret;
            }
            set
            {
                bonusIsSecret = value;
            }
        }

    }}
