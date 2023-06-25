using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("AlignmentRank", "")]
    public class AlignmentRank : IDataObject , IIndexedData
    {        public const string MODULE = "AlignmentRank";

        public int Id => (int)id;

        public int id;
        public uint orderId;
        public uint nameId;
        public uint descriptionId;
        public int minimumAlignment;
        public List<int> gifts;

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
        public uint OrderId
        {
            get
            {
                return orderId;
            }
            set
            {
                orderId = value;
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
        public int MinimumAlignment
        {
            get
            {
                return minimumAlignment;
            }
            set
            {
                minimumAlignment = value;
            }
        }
        [D2OIgnore]
        public List<int> Gifts
        {
            get
            {
                return gifts;
            }
            set
            {
                gifts = value;
            }
        }

    }}
