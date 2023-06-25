using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MapObstacleUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 7517;
        public override ushort MessageId => Id;

        public MapObstacle[] obstacles;

        public MapObstacleUpdateMessage()
        {
        }
        public MapObstacleUpdateMessage(MapObstacle[] obstacles)
        {
            this.obstacles = obstacles;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)obstacles.Length);
            for (uint _i1 = 0;_i1 < obstacles.Length;_i1++)
            {
                (obstacles[_i1] as MapObstacle).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            MapObstacle _item1 = null;
            uint _obstaclesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _obstaclesLen;_i1++)
            {
                _item1 = new MapObstacle();
                _item1.Deserialize(reader);
                obstacles[_i1] = _item1;
            }

        }


    }
}








