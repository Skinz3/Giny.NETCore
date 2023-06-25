using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("BreachDungeonModificator", "")]
    public class BreachDungeonModificator : IDataObject , IIndexedData
    {        public const string MODULE = "BreachDungeonModificators";

        public int Id => (int)id;

        public uint id;
        public uint modificatorId;
        public string criterion;
        public double additionalRewardPercent;
        public double score;
        public bool isPositiveForPlayers;
        public string tooltipBaseline;

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
        public uint ModificatorId
        {
            get
            {
                return modificatorId;
            }
            set
            {
                modificatorId = value;
            }
        }
        [D2OIgnore]
        public string Criterion
        {
            get
            {
                return criterion;
            }
            set
            {
                criterion = value;
            }
        }
        [D2OIgnore]
        public double AdditionalRewardPercent
        {
            get
            {
                return additionalRewardPercent;
            }
            set
            {
                additionalRewardPercent = value;
            }
        }
        [D2OIgnore]
        public double Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        [D2OIgnore]
        public bool IsPositiveForPlayers
        {
            get
            {
                return isPositiveForPlayers;
            }
            set
            {
                isPositiveForPlayers = value;
            }
        }
        [D2OIgnore]
        public string TooltipBaseline
        {
            get
            {
                return tooltipBaseline;
            }
            set
            {
                tooltipBaseline = value;
            }
        }

    }}
