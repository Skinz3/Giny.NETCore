using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("QuestCategory", "")]
    public class QuestCategory : IDataObject , IIndexedData
    {        public const string MODULE = "QuestCategory";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint order;
        public List<uint> questIds;

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
        public uint Order
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
        public List<uint> QuestIds
        {
            get
            {
                return questIds;
            }
            set
            {
                questIds = value;
            }
        }

    }}
