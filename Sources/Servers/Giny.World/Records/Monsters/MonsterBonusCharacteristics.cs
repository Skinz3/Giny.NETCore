using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giny.World.Records.Monsters
{
    [ProtoContract]
    public class MonsterBonusCharacteristics
    {
        [ProtoMember(1)]
        public int LifePoints
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public int Strength
        {
            get;
            set;
        }
        [ProtoMember(3)]
        public int Wisdom
        {
            get;
            set;
        }
        [ProtoMember(4)]
        public int Chance
        {
            get;
            set;
        }
        [ProtoMember(5)]
        public int Agility
        {
            get;
            set;
        }
        [ProtoMember(6)]
        public int Intelligence
        {
            get;
            set;
        }
        [ProtoMember(7)]
        public int EarthResistance
        {
            get;
            set;
        }
        [ProtoMember(8)]
        public int FireResistance
        {
            get;
            set;
        }
        [ProtoMember(9)]
        public int WaterResistance
        {
            get;
            set;
        }
        [ProtoMember(10)]
        public int AirResistance
        {
            get;
            set;
        }
        [ProtoMember(11)]
        public int NeutralResistance
        {
            get;
            set;
        }
        [ProtoMember(12)]
        public int TackleEvade
        {
            get;
            set;
        }
        [ProtoMember(13)]
        public int TackleBlock
        {
            get;
            set;
        }
        [ProtoMember(14)]
        public int BonusEarthDamage
        {
            get;
            set;
        }
        [ProtoMember(15)]
        public int BonusFireDamage
        {
            get;
            set;
        }
        [ProtoMember(16)]
        public int BonusWaterDamage
        {
            get;
            set;
        }
        [ProtoMember(17)]
        public int BonusAirDamage
        {
            get;
            set;
        }
        [ProtoMember(18)]
        public int APRemoval
        {
            get;
            set;
        }
    }
}
