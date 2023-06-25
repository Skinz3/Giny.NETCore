using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("VeteranReward", "")]
    public class VeteranReward : IDataObject , IIndexedData
    {        public const string MODULE = "VeteranRewards";

        public int Id => (int)id;

        public int id;
        public uint requiredSubDays;
        public uint itemGID;
        public uint itemQuantity;

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
        public uint RequiredSubDays
        {
            get
            {
                return requiredSubDays;
            }
            set
            {
                requiredSubDays = value;
            }
        }
        [D2OIgnore]
        public uint ItemGID
        {
            get
            {
                return itemGID;
            }
            set
            {
                itemGID = value;
            }
        }
        [D2OIgnore]
        public uint ItemQuantity
        {
            get
            {
                return itemQuantity;
            }
            set
            {
                itemQuantity = value;
            }
        }

    }}
