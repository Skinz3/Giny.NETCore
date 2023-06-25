using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MapComplementaryInformationsDataInHouseMessage : MapComplementaryInformationsDataMessage  
    { 
        public new const ushort Id = 7853;
        public override ushort MessageId => Id;

        public HouseInformationsInside currentHouse;

        public MapComplementaryInformationsDataInHouseMessage()
        {
        }
        public MapComplementaryInformationsDataInHouseMessage(HouseInformationsInside currentHouse,short subAreaId,double mapId,HouseInformations[] houses,GameRolePlayActorInformations[] actors,InteractiveElement[] interactiveElements,StatedElement[] statedElements,MapObstacle[] obstacles,FightCommonInformations[] fights,bool hasAggressiveMonsters,FightStartingPositions fightStartPositions)
        {
            this.currentHouse = currentHouse;
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
            currentHouse.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            currentHouse = new HouseInformationsInside();
            currentHouse.Deserialize(reader);
        }


    }
}








