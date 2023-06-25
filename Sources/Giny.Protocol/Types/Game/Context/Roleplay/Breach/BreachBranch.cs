using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class BreachBranch  
    { 
        public const ushort Id = 9447;
        public virtual ushort TypeId => Id;

        public byte room;
        public int element;
        public MonsterInGroupLightInformations[] bosses;
        public double map;
        public short score;
        public short relativeScore;
        public MonsterInGroupLightInformations[] monsters;

        public BreachBranch()
        {
        }
        public BreachBranch(byte room,int element,MonsterInGroupLightInformations[] bosses,double map,short score,short relativeScore,MonsterInGroupLightInformations[] monsters)
        {
            this.room = room;
            this.element = element;
            this.bosses = bosses;
            this.map = map;
            this.score = score;
            this.relativeScore = relativeScore;
            this.monsters = monsters;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (room < 0)
            {
                throw new System.Exception("Forbidden value (" + room + ") on element room.");
            }

            writer.WriteByte((byte)room);
            if (element < 0)
            {
                throw new System.Exception("Forbidden value (" + element + ") on element element.");
            }

            writer.WriteInt((int)element);
            writer.WriteShort((short)bosses.Length);
            for (uint _i3 = 0;_i3 < bosses.Length;_i3++)
            {
                (bosses[_i3] as MonsterInGroupLightInformations).Serialize(writer);
            }

            if (map < 0 || map > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + map + ") on element map.");
            }

            writer.WriteDouble((double)map);
            writer.WriteShort((short)score);
            writer.WriteShort((short)relativeScore);
            writer.WriteShort((short)monsters.Length);
            for (uint _i7 = 0;_i7 < monsters.Length;_i7++)
            {
                (monsters[_i7] as MonsterInGroupLightInformations).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            MonsterInGroupLightInformations _item3 = null;
            MonsterInGroupLightInformations _item7 = null;
            room = (byte)reader.ReadByte();
            if (room < 0)
            {
                throw new System.Exception("Forbidden value (" + room + ") on element of BreachBranch.room.");
            }

            element = (int)reader.ReadInt();
            if (element < 0)
            {
                throw new System.Exception("Forbidden value (" + element + ") on element of BreachBranch.element.");
            }

            uint _bossesLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _bossesLen;_i3++)
            {
                _item3 = new MonsterInGroupLightInformations();
                _item3.Deserialize(reader);
                bosses[_i3] = _item3;
            }

            map = (double)reader.ReadDouble();
            if (map < 0 || map > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + map + ") on element of BreachBranch.map.");
            }

            score = (short)reader.ReadShort();
            relativeScore = (short)reader.ReadShort();
            uint _monstersLen = (uint)reader.ReadUShort();
            for (uint _i7 = 0;_i7 < _monstersLen;_i7++)
            {
                _item7 = new MonsterInGroupLightInformations();
                _item7.Deserialize(reader);
                monsters[_i7] = _item7;
            }

        }


    }
}








