using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MapComplementaryInformationsBreachMessage : MapComplementaryInformationsDataMessage  
    { 
        public new const ushort Id = 8282;
        public override ushort MessageId => Id;

        public int floor;
        public byte room;
        public short infinityMode;
        public BreachBranch[] branches;

        public MapComplementaryInformationsBreachMessage()
        {
        }
        public MapComplementaryInformationsBreachMessage(int floor,byte room,short infinityMode,BreachBranch[] branches,short subAreaId,double mapId,HouseInformations[] houses,GameRolePlayActorInformations[] actors,InteractiveElement[] interactiveElements,StatedElement[] statedElements,MapObstacle[] obstacles,FightCommonInformations[] fights,bool hasAggressiveMonsters,FightStartingPositions fightStartPositions)
        {
            this.floor = floor;
            this.room = room;
            this.infinityMode = infinityMode;
            this.branches = branches;
            this.subAreaId = subAreaId;
            this.mapId = mapId;
            this.houses = houses;
            this.actors = actors;
            this.interactiveElements = interactiveElements;
            this.statedElements = statedElements;
            this.obstacles = obstacles;
            this.fights = fights;
            this.hasAggressiveMonsters = hasAggressiveMonsters;
            this.fightStartPositions = fightStartPositions;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (floor < 0)
            {
                throw new System.Exception("Forbidden value (" + floor + ") on element floor.");
            }

            writer.WriteVarInt((int)floor);
            if (room < 0)
            {
                throw new System.Exception("Forbidden value (" + room + ") on element room.");
            }

            writer.WriteByte((byte)room);
            if (infinityMode < 0)
            {
                throw new System.Exception("Forbidden value (" + infinityMode + ") on element infinityMode.");
            }

            writer.WriteShort((short)infinityMode);
            writer.WriteShort((short)branches.Length);
            for (uint _i4 = 0;_i4 < branches.Length;_i4++)
            {
                writer.WriteShort((short)(branches[_i4] as BreachBranch).TypeId);
                (branches[_i4] as BreachBranch).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id4 = 0;
            BreachBranch _item4 = null;
            base.Deserialize(reader);
            floor = (int)reader.ReadVarUhInt();
            if (floor < 0)
            {
                throw new System.Exception("Forbidden value (" + floor + ") on element of MapComplementaryInformationsBreachMessage.floor.");
            }

            room = (byte)reader.ReadByte();
            if (room < 0)
            {
                throw new System.Exception("Forbidden value (" + room + ") on element of MapComplementaryInformationsBreachMessage.room.");
            }

            infinityMode = (short)reader.ReadShort();
            if (infinityMode < 0)
            {
                throw new System.Exception("Forbidden value (" + infinityMode + ") on element of MapComplementaryInformationsBreachMessage.infinityMode.");
            }

            uint _branchesLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _branchesLen;_i4++)
            {
                _id4 = (uint)reader.ReadUShort();
                _item4 = ProtocolTypeManager.GetInstance<BreachBranch>((short)_id4);
                _item4.Deserialize(reader);
                branches[_i4] = _item4;
            }

        }


    }
}








