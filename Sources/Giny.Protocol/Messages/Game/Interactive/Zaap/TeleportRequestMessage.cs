using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TeleportRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 7877;
        public override ushort MessageId => Id;

        public byte sourceType;
        public byte destinationType;
        public double mapId;

        public TeleportRequestMessage()
        {
        }
        public TeleportRequestMessage(byte sourceType,byte destinationType,double mapId)
        {
            this.sourceType = sourceType;
            this.destinationType = destinationType;
            this.mapId = mapId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)sourceType);
            writer.WriteByte((byte)destinationType);
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
        }
        public override void Deserialize(IDataReader reader)
        {
            sourceType = (byte)reader.ReadByte();
            if (sourceType < 0)
            {
                throw new System.Exception("Forbidden value (" + sourceType + ") on element of TeleportRequestMessage.sourceType.");
            }

            destinationType = (byte)reader.ReadByte();
            if (destinationType < 0)
            {
                throw new System.Exception("Forbidden value (" + destinationType + ") on element of TeleportRequestMessage.destinationType.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of TeleportRequestMessage.mapId.");
            }

        }


    }
}








