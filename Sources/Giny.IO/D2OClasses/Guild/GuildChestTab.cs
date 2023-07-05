using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{
    [D2OClass("GuildChestTab", "")]
    public class GuildChestTab : IDataObject, IIndexedData
    {
        public const string MODULE = "GuildChestTabs";

        public int Id => throw new NotImplementedException();

        public int tabId;
        public uint nameId;
        public uint index;
        public uint gfxId;
        public int serverType;
        public uint cost;
        public uint seniority;
        public uint openRight;
        public uint dropRight;
        public uint takeRight;

        [D2OIgnore]
        public int TabId
        {
            get
            {
                return tabId;
            }
            set
            {
                tabId = value;
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
        public uint Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
            }
        }
        [D2OIgnore]
        public uint GfxId
        {
            get
            {
                return gfxId;
            }
            set
            {
                gfxId = value;
            }
        }
        [D2OIgnore]
        public int ServerType
        {
            get
            {
                return serverType;
            }
            set
            {
                serverType = value;
            }
        }
        [D2OIgnore]
        public uint Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = value;
            }
        }
        [D2OIgnore]
        public uint Seniority
        {
            get
            {
                return seniority;
            }
            set
            {
                seniority = value;
            }
        }
        [D2OIgnore]
        public uint OpenRight
        {
            get
            {
                return openRight;
            }
            set
            {
                openRight = value;
            }
        }
        [D2OIgnore]
        public uint DropRight
        {
            get
            {
                return dropRight;
            }
            set
            {
                dropRight = value;
            }
        }
        [D2OIgnore]
        public uint TakeRight
        {
            get
            {
                return takeRight;
            }
            set
            {
                takeRight = value;
            }
        }

    }
}

