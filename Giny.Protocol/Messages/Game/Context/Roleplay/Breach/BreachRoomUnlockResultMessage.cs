using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachRoomUnlockResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 5488;
        public override ushort MessageId => Id;

        public byte roomId;
        public byte result;

        public BreachRoomUnlockResultMessage()
        {
        }
        public BreachRoomUnlockResultMessage(byte roomId,byte result)
        {
            this.roomId = roomId;
            this.result = result;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (roomId < 0)
            {
                throw new System.Exception("Forbidden value (" + roomId + ") on element roomId.");
            }

            writer.WriteByte((byte)roomId);
            writer.WriteByte((byte)result);
        }
        public override void Deserialize(IDataReader reader)
        {
            roomId = (byte)reader.ReadByte();
            if (roomId < 0)
            {
                throw new System.Exception("Forbidden value (" + roomId + ") on element of BreachRoomUnlockResultMessage.roomId.");
            }

            result = (byte)reader.ReadByte();
            if (result < 0)
            {
                throw new System.Exception("Forbidden value (" + result + ") on element of BreachRoomUnlockResultMessage.result.");
            }

        }


    }
}








