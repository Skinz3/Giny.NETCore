using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MonsterDropCoefficient", "")]
    public class MonsterDropCoefficient : IDataObject , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint monsterId;
        public uint monsterGrade;
        public double dropCoefficient;
        public string criteria;

        [D2OIgnore]
        public uint MonsterId
        {
            get
            {
                return monsterId;
            }
            set
            {
                monsterId = value;
            }
        }
        [D2OIgnore]
        public uint MonsterGrade
        {
            get
            {
                return monsterGrade;
            }
            set
            {
                monsterGrade = value;
            }
        }
        [D2OIgnore]
        public double DropCoefficient
        {
            get
            {
                return dropCoefficient;
            }
            set
            {
                dropCoefficient = value;
            }
        }
        [D2OIgnore]
        public string Criteria
        {
            get
            {
                return criteria;
            }
            set
            {
                criteria = value;
            }
        }

    }}
