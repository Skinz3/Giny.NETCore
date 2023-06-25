using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Smiley", "")]
    public class Smiley : IDataObject , IIndexedData
    {        public const string MODULE = "Smileys";

        public int Id => (int)id;

        public uint id;
        public uint order;
        public string gfxId;
        public bool forPlayers;
        public List<string> triggers;
        public uint referenceId;
        public uint categoryId;

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
        public string GfxId
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
        public bool ForPlayers
        {
            get
            {
                return forPlayers;
            }
            set
            {
                forPlayers = value;
            }
        }
        [D2OIgnore]
        public List<string> Triggers
        {
            get
            {
                return triggers;
            }
            set
            {
                triggers = value;
            }
        }
        [D2OIgnore]
        public uint ReferenceId
        {
            get
            {
                return referenceId;
            }
            set
            {
                referenceId = value;
            }
        }
        [D2OIgnore]
        public uint CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                categoryId = value;
            }
        }

    }}
