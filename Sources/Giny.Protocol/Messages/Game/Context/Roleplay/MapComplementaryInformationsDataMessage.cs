using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MapComplementaryInformationsDataMessage : NetworkMessage  
    { 
        public  const ushort Id = 2203;
        public override ushort MessageId => Id;

        public short subAreaId;
        public double mapId;
        public HouseInformations[] houses;
        public GameRolePlayActorInformations[] actors;
        public InteractiveElement[] interactiveElements;
        public StatedElement[] statedElements;
        public MapObstacle[] obstacles;
        public FightCommonInformations[] fights;
        public bool hasAggressiveMonsters;
        public FightStartingPositions fightStartPositions;

        public MapComplementaryInformationsDataMessage()
        {
        }
        public MapComplementaryInformationsDataMessage(short subAreaId,double mapId,HouseInformations[] houses,GameRolePlayActorInformations[] actors,InteractiveElement[] interactiveElements,StatedElement[] statedElements,MapObstacle[] obstacles,FightCommonInformations[] fights,bool hasAggressiveMonsters,FightStartingPositions fightStartPositions)
        {
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
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            writer.WriteShort((short)houses.Length);
            for (uint _i3 = 0;_i3 < houses.Length;_i3++)
            {
                writer.WriteShort((short)(houses[_i3] as HouseInformations).TypeId);
                (houses[_i3] as HouseInformations).Serialize(writer);
            }

            writer.WriteShort((short)actors.Length);
            for (uint _i4 = 0;_i4 < actors.Length;_i4++)
            {
                writer.WriteShort((short)(actors[_i4] as GameRolePlayActorInformations).TypeId);
                (actors[_i4] as GameRolePlayActorInformations).Serialize(writer);
            }

            writer.WriteShort((short)interactiveElements.Length);
            for (uint _i5 = 0;_i5 < interactiveElements.Length;_i5++)
            {
                writer.WriteShort((short)(interactiveElements[_i5] as InteractiveElement).TypeId);
                (interactiveElements[_i5] as InteractiveElement).Serialize(writer);
            }

            writer.WriteShort((short)statedElements.Length);
            for (uint _i6 = 0;_i6 < statedElements.Length;_i6++)
            {
                (statedElements[_i6] as StatedElement).Serialize(writer);
            }

            writer.WriteShort((short)obstacles.Length);
            for (uint _i7 = 0;_i7 < obstacles.Length;_i7++)
            {
                (obstacles[_i7] as MapObstacle).Serialize(writer);
            }

            writer.WriteShort((short)fights.Length);
            for (uint _i8 = 0;_i8 < fights.Length;_i8++)
            {
                (fights[_i8] as FightCommonInformations).Serialize(writer);
            }

            writer.WriteBoolean((bool)hasAggressiveMonsters);
            fightStartPositions.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id3 = 0;
            HouseInformations _item3 = null;
            uint _id4 = 0;
            GameRolePlayActorInformations _item4 = null;
            uint _id5 = 0;
            InteractiveElement _item5 = null;
            StatedElement _item6 = null;
            MapObstacle _item7 = null;
            FightCommonInformations _item8 = null;
            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of MapComplementaryInformationsDataMessage.subAreaId.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of MapComplementaryInformationsDataMessage.mapId.");
            }

            uint _housesLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _housesLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<HouseInformations>((short)_id3);
                _item3.Deserialize(reader);
                houses[_i3] = _item3;
            }

            uint _actorsLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _actorsLen;_i4++)
            {
                _id4 = (uint)reader.ReadUShort();
                _item4 = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>((short)_id4);
                _item4.Deserialize(reader);
                actors[_i4] = _item4;
            }

            uint _interactiveElementsLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _interactiveElementsLen;_i5++)
            {
                _id5 = (uint)reader.ReadUShort();
                _item5 = ProtocolTypeManager.GetInstance<InteractiveElement>((short)_id5);
                _item5.Deserialize(reader);
                interactiveElements[_i5] = _item5;
            }

            uint _statedElementsLen = (uint)reader.ReadUShort();
            for (uint _i6 = 0;_i6 < _statedElementsLen;_i6++)
            {
                _item6 = new StatedElement();
                _item6.Deserialize(reader);
                statedElements[_i6] = _item6;
            }

            uint _obstaclesLen = (uint)reader.ReadUShort();
            for (uint _i7 = 0;_i7 < _obstaclesLen;_i7++)
            {
                _item7 = new MapObstacle();
                _item7.Deserialize(reader);
                obstacles[_i7] = _item7;
            }

            uint _fightsLen = (uint)reader.ReadUShort();
            for (uint _i8 = 0;_i8 < _fightsLen;_i8++)
            {
                _item8 = new FightCommonInformations();
                _item8.Deserialize(reader);
                fights[_i8] = _item8;
            }

            hasAggressiveMonsters = (bool)reader.ReadBoolean();
            fightStartPositions = new FightStartingPositions();
            fightStartPositions.Deserialize(reader);
        }


    }
}








