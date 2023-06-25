using System;
using Giny.Core.IO.Interfaces;
using Giny.IO.D2O;
using Giny.IO.D2OTypes;
using System.Collections.Generic;

namespace Giny.IO.D2OClasses
{    [D2OClass("MonsterDrop", "")]
    public class MonsterDrop : IDataObject , IIndexedData
    {
        public int Id => throw new NotImplementedException();

        public uint dropId;
        public int monsterId;
        public int objectId;
        public double percentDropForGrade1;
        public double percentDropForGrade2;
        public double percentDropForGrade3;
        public double percentDropForGrade4;
        public double percentDropForGrade5;
        public int count;
        public string criteria;
        public bool hasCriteria;
        public List<MonsterDropCoefficient> specificDropCoefficient;

        [D2OIgnore]
        public uint DropId
        {
            get
            {
                return dropId;
            }
            set
            {
                dropId = value;
            }
        }
        [D2OIgnore]
        public int MonsterId
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
        public int ObjectId
        {
            get
            {
                return objectId;
            }
            set
            {
                objectId = value;
            }
        }
        [D2OIgnore]
        public double PercentDropForGrade1
        {
            get
            {
                return percentDropForGrade1;
            }
            set
            {
                percentDropForGrade1 = value;
            }
        }
        [D2OIgnore]
        public double PercentDropForGrade2
        {
            get
            {
                return percentDropForGrade2;
            }
            set
            {
                percentDropForGrade2 = value;
            }
        }
        [D2OIgnore]
        public double PercentDropForGrade3
        {
            get
            {
                return percentDropForGrade3;
            }
            set
            {
                percentDropForGrade3 = value;
            }
        }
        [D2OIgnore]
        public double PercentDropForGrade4
        {
            get
            {
                return percentDropForGrade4;
            }
            set
            {
                percentDropForGrade4 = value;
            }
        }
        [D2OIgnore]
        public double PercentDropForGrade5
        {
            get
            {
                return percentDropForGrade5;
            }
            set
            {
                percentDropForGrade5 = value;
            }
        }
        [D2OIgnore]
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                count = value;
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
        [D2OIgnore]
        public bool HasCriteria
        {
            get
            {
                return hasCriteria;
            }
            set
            {
                hasCriteria = value;
            }
        }
        [D2OIgnore]
        public List<MonsterDropCoefficient> SpecificDropCoefficient
        {
            get
            {
                return specificDropCoefficient;
            }
            set
            {
                specificDropCoefficient = value;
            }
        }

    }}
