using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("RandomDropItem", "")]
    public class RandomDropItem : IDataObject , IIndexedData
    {
        public int Id => (int)id;

        public uint id;
        public uint itemId;
        public double probability;
        public uint minQuantity;
        public uint maxQuantity;

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
        public uint ItemId
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
        [D2OIgnore]
        public double Probability
        {
            get
            {
                return probability;
            }
            set
            {
                probability = value;
            }
        }
        [D2OIgnore]
        public uint MinQuantity
        {
            get
            {
                return minQuantity;
            }
            set
            {
                minQuantity = value;
            }
        }
        [D2OIgnore]
        public uint MaxQuantity
        {
            get
            {
                return maxQuantity;
            }
            set
            {
                maxQuantity = value;
            }
        }

    }}
