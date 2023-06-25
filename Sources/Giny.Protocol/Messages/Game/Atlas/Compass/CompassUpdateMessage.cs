using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CompassUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 1473;
        public override ushort MessageId => Id;

        public byte type;
        public MapCoordinates coords;

        public CompassUpdateMessage()
        {
        }
        public CompassUpdateMessage(byte type,MapCoordinates coords)
        {
            this.type = type;
            this.coords = coords;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
            writer.WriteShort((short)coords.TypeId);
            coords.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of CompassUpdateMessage.type.");
            }

            uint _id2 = (uint)reader.ReadUShort();
            coords = ProtocolTypeManager.GetInstance<MapCoordinates>((short)_id2);
            coords.Deserialize(reader);
        }


    }
}








