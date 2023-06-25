using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("LegendaryTreasureHunt", "")]
    public class LegendaryTreasureHunt : IDataObject , IIndexedData
    {        public const string MODULE = "LegendaryTreasureHunts";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint level;
        public uint chestId;
        public uint monsterId;
        public uint mapItemId;
        public double xpRatio;

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
        public uint ChestId
        {
            get
            {
                return chestId;
            }
            set
            {
                chestId = value;
            }
        }
        [D2OIgnore]
        public uint MonsterId
        {
            get
            {
                return monsterId;
            }
            set
            {
                monsterId = value;
            }
        }
        [D2OIgnore]
        public uint MapItemId
        {
            get
            {
                return mapItemId;
            }
            set
            {
                mapItemId = value;
            }
        }
        [D2OIgnore]
        public double XpRatio
        {
            get
            {
                return xpRatio;
            }
            set
            {
                xpRatio = value;
            }
        }

    }}
