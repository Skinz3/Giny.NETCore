using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("GuildRank", "")]
    public class GuildRank : IDataObject , IIndexedData
    {        public const string MODULE = "GuildRanks";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int order;
        public bool isModifiable;
        public uint gfxId;

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
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;
            }
        }
        [D2OIgnore]
        public bool IsModifiable
        {
            get
            {
                return isModifiable;
            }
            set
            {
                isModifiable = value;
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

    }}
