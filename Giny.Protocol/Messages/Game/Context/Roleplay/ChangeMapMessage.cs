using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ChangeMapMessage : NetworkMessage  
    { 
        public  const ushort Id = 5275;
        public override ushort MessageId => Id;

        public double mapId;
        public bool autopilot;

        public ChangeMapMessage()
        {
        }
        public ChangeMapMessage(double mapId,bool autopilot)
        {
            this.mapId = mapId;
            this.autopilot = autopilot;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            writer.WriteBoolean((bool)autopilot);
        }
        public override void Deserialize(IDataReader reader)
        {
            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of ChangeMapMessage.mapId.");
            }

            autopilot = (bool)reader.ReadBoolean();
        }


    }
}








