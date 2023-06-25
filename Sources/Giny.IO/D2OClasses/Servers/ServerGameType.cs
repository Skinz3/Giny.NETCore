using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("ServerGameType", "")]
    public class ServerGameType : IDataObject , IIndexedData
    {        public const string MODULE = "ServerGameTypes";

        public int Id => (int)id;

        public int id;
        public bool selectableByPlayer;
        public uint nameId;
        public uint rulesId;
        public uint descriptionId;

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
        public bool SelectableByPlayer
        {
            get
            {
                return selectableByPlayer;
            }
            set
            {
                selectableByPlayer = value;
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
        public uint RulesId
        {
            get
            {
                return rulesId;
            }
            set
            {
                rulesId = value;
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

    }}
