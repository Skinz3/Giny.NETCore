using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("SmileyPack", "")]
    public class SmileyPack : IDataObject , IIndexedData
    {        public const string MODULE = "SmileyPacks";

        public int Id => (int)id;

        public uint id;
        public uint nameId;
        public uint order;
        public List<uint> smileys;

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
        public List<uint> Smileys
        {
            get
            {
                return smileys;
            }
            set
            {
                smileys = value;
            }
        }

    }}
