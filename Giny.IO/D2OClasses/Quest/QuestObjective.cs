using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("QuestObjective", "")]
    public class QuestObjective : IDataObject , IIndexedData
    {        public const string MODULE = "QuestObjectives";

        public int Id => (int)id;

        public uint id;
        public uint stepId;
        public uint typeId;
        public int dialogId;
        public QuestObjectiveParameters parameters;
        public Point coords;
        public double mapId;

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
        public uint StepId
        {
            get
            {
                return stepId;
            }
            set
            {
                stepId = value;
            }
        }
        [D2OIgnore]
        public uint TypeId
        {
            get
            {
                return typeId;
            }
            set
            {
                typeId = value;
            }
        }
        [D2OIgnore]
        public int DialogId
        {
            get
            {
                return dialogId;
            }
            set
            {
                dialogId = value;
            }
        }
        [D2OIgnore]
        public QuestObjectiveParameters Parameters
        {
            get
            {
                return parameters;
            }
            set
            {
                parameters = value;
            }
        }
        [D2OIgnore]
        public Point Coords
        {
            get
            {
                return coords;
            }
            set
            {
                coords = value;
            }
        }
        [D2OIgnore]
        public double MapId
        {
            get
            {
                return mapId;
            }
            set
            {
                mapId = value;
            }
        }

    }}
