using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MountInformationInPaddockRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 4981;
        public override ushort MessageId => Id;

        public int mapRideId;

        public MountInformationInPaddockRequestMessage()
        {
        }
        public MountInformationInPaddockRequestMessage(int mapRideId)
        {
            this.mapRideId = mapRideId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt((int)mapRideId);
        }
        public override void Deserialize(IDataReader reader)
        {
            mapRideId = (int)reader.ReadVarInt();
        }


    }
}








