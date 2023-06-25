using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("QuestObjectiveParameters", "")]
    public class QuestObjectiveParameters : Proxy , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint numParams;
        public int parameter0;
        public int parameter1;
        public int parameter2;
        public int parameter3;
        public int parameter4;
        public bool dungeonOnly;

        [D2OIgnore]
        public uint NumParams
        {
            get
            {
                return numParams;
            }
            set
            {
                numParams = value;
            }
        }
        [D2OIgnore]
        public int Parameter0
        {
            get
            {
                return parameter0;
            }
            set
            {
                parameter0 = value;
            }
        }
        [D2OIgnore]
        public int Parameter1
        {
            get
            {
                return parameter1;
            }
            set
            {
                parameter1 = value;
            }
        }
        [D2OIgnore]
        public int Parameter2
        {
            get
            {
                return parameter2;
            }
            set
            {
                parameter2 = value;
            }
        }
        [D2OIgnore]
        public int Parameter3
        {
            get
            {
                return parameter3;
            }
            set
            {
                parameter3 = value;
            }
        }
        [D2OIgnore]
        public int Parameter4
        {
            get
            {
                return parameter4;
            }
            set
            {
                parameter4 = value;
            }
        }
        [D2OIgnore]
        public bool DungeonOnly
        {
            get
            {
                return dungeonOnly;
            }
            set
            {
                dungeonOnly = value;
            }
        }

    }}
