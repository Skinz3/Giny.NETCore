using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DisplayNumericalValuePaddockMessage : NetworkMessage  
    { 
        public  const ushort Id = 5321;
        public override ushort MessageId => Id;

        public int rideId;
        public int value;
        public byte type;

        public DisplayNumericalValuePaddockMessage()
        {
        }
        public DisplayNumericalValuePaddockMessage(int rideId,int value,byte type)
        {
            this.rideId = rideId;
            this.value = value;
            this.type = type;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)rideId);
            writer.WriteInt((int)value);
            writer.WriteByte((byte)type);
        }
        public override void Deserialize(IDataReader reader)
        {
            rideId = (int)reader.ReadInt();
            value = (int)reader.ReadInt();
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of DisplayNumericalValuePaddockMessage.type.");
            }

        }


    }
}








