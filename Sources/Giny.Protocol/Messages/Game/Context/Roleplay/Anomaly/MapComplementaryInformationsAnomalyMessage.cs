using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MapComplementaryInformationsAnomalyMessage : MapComplementaryInformationsDataMessage  
    { 
        public new const ushort Id = 3607;
        public override ushort MessageId => Id;

        public short level;
        public long closingTime;

        public MapComplementaryInformationsAnomalyMessage()
        {
        }
        public MapComplementaryInformationsAnomalyMessage(short level,long closingTime,short subAreaId,double mapId,HouseInformations[] houses,GameRolePlayActorInformations[] actors,InteractiveElement[] interactiveElements,StatedElement[] statedElements,MapObstacle[] obstacles,FightCommonInformations[] fights,bool hasAggressiveMonsters,FightStartingPositions fightStartPositions)
        {
            this.level = level;
            this.closingTime = closingTime;
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
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteVarShort((short)level);
            if (closingTime < 0 || closingTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + closingTime + ") on element closingTime.");
            }

            writer.WriteVarLong((long)closingTime);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            level = (short)reader.ReadVarUhShort();
            if (level < 0)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of MapComplementaryInformationsAnomalyMessage.level.");
            }

            closingTime = (long)reader.ReadVarUhLong();
            if (closingTime < 0 || closingTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + closingTime + ") on element of MapComplementaryInformationsAnomalyMessage.closingTime.");
            }

        }


    }
}








