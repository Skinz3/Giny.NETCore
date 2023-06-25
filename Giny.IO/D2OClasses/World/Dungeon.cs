using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("Dungeon", "")]
    public class Dungeon : IDataObject , IIndexedData
    {        public const string MODULE = "Dungeons";

        public int Id => (int)id;

        public int id;
        public uint nameId;
        public int optimalPlayerLevel;
        public List<double> mapIds;
        public double entranceMapId;
        public double exitMapId;

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
        public int OptimalPlayerLevel
        {
            get
            {
                return optimalPlayerLevel;
            }
            set
            {
                optimalPlayerLevel = value;
            }
        }
        [D2OIgnore]
        public List<double> MapIds
        {
            get
            {
                return mapIds;
            }
            set
            {
                mapIds = value;
            }
        }
        [D2OIgnore]
        public double EntranceMapId
        {
            get
            {
                return entranceMapId;
            }
            set
            {
                entranceMapId = value;
            }
        }
        [D2OIgnore]
        public double ExitMapId
        {
            get
            {
                return exitMapId;
            }
            set
            {
                exitMapId = value;
            }
        }

    }}
