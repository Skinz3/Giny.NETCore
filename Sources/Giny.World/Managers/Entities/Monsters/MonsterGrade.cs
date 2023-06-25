using Giny.ORM.Attributes;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Managers.Monsters
{
    [ProtoContract]
    public class MonsterGrade
    {
        [ProtoMember(1)]
        public byte GradeId
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public short Level
        {
            get;
            set;
        }
      
        [ProtoMember(3)]
        public short Vitality
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public short ApDodge
        {
            get;
            set;
        }
        [ProtoMember(5)]
        public short MpDodge
        {
            get;
            set;
        }
        [ProtoMember(6)]
        public short Wisdom
        {
            get;
            set;
        }
        [ProtoMember(7)]
        public short EarthResistance
        {
            get;
            set;
        }
        [ProtoMember(8)]
        public short AirResistance
        {
            get;
            set;
        }
        [ProtoMember(9)]
        public short FireResistance
        {
            get;
            set;
        }
        [ProtoMember(10)]
        public short WaterResistance
        {
            get;
            set;
        }
        [ProtoMember(11)]
        public short NeutralResistance
        {
            get;
            set;
        }
        [ProtoMember(12)]
        public long GradeXp
        {
            get;
            set;
        }
        [ProtoMember(13)]
        public int LifePoints
        {
            get;
            set;
        }
        [ProtoMember(14)]
        public short ActionPoints
        {
            get;
            set;
        }
        [ProtoMember(15)]
        public short MovementPoints
        {
            get;
            set;
        }
        [ProtoMember(16)]
        public short DamageReflect
        {
            get;
            set;
        }
        [ProtoMember(17)]
        public short HiddenLevel
        {
            get;
            set;
        }
        [ProtoMember(18)]
        public short Strength
        {
            get;
            set;
        }
        [ProtoMember(19)]
        public short Intelligence
        {
            get;
            set;
        }
        [ProtoMember(20)]
        public short Chance
        {
            get;
            set;
        }
        [ProtoMember(21)]
        public short Agility
        {
            get;
            set;
        }
        [ProtoMember(22)]
        public int StartingSpellLevelId
        {
            get;
            set;
        }
    }
}
