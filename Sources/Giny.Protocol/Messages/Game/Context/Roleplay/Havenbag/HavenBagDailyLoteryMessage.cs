using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HavenBagDailyLoteryMessage : NetworkMessage  
    { 
        public  const ushort Id = 2601;
        public override ushort MessageId => Id;

        public byte returnType;
        public string gameActionId;

        public HavenBagDailyLoteryMessage()
        {
        }
        public HavenBagDailyLoteryMessage(byte returnType,string gameActionId)
        {
            this.returnType = returnType;
            this.gameActionId = gameActionId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)returnType);
            writer.WriteUTF((string)gameActionId);
        }
        public override void Deserialize(IDataReader reader)
        {
            returnType = (byte)reader.ReadByte();
            if (returnType < 0)
            {
                throw new System.Exception("Forbidden value (" + returnType + ") on element of HavenBagDailyLoteryMessage.returnType.");
            }

            gameActionId = (string)reader.ReadUTF();
        }


    }
}








