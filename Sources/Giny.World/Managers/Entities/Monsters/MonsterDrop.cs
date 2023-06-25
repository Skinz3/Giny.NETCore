using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Entities.Monsters
{
    [ProtoContract]
    public class MonsterDrop
    {
        [ProtoMember(1)]
        public int ItemGId
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public double PercentDropForGrade1
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public double PercentDropForGrade2
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public double PercentDropForGrade3
        {
            get;
            set;
        }
        [ProtoMember(5)]
        public double PercentDropForGrade4
        {
            get;
            set;
        }
        [ProtoMember(6)]
        public double PercentDropForGrade5
        {
            get;
            set;
        }
        [ProtoMember(7)]
        public int DropLimit
        {
            get;
            set;
        }
        [ProtoMember(8)]
        public string criteria
        {
            get;
            set;
        }
        [ProtoMember(9)]
        public bool HasCriteria
        {
            get;
            set;
        }
        public int ProspectingLock
        {
            get;
            set;
        } = 100;
        public int RollsCounter
        {
            get;
            set;
        } = 1;

        public double GetDropRate(int grade)
        {
            if (grade <= 1)
                return PercentDropForGrade1;
            else if (grade == 2)
                return PercentDropForGrade2;
            else if (grade == 3)
                return PercentDropForGrade3;
            else if (grade == 4)
                return PercentDropForGrade4;
            else
                return PercentDropForGrade5;
        }
    }
}
