using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeMountSterilizeFromPaddockMessage : NetworkMessage  
    { 
        public  const ushort Id = 7786;
        public override ushort MessageId => Id;

        public string name;
        public short worldX;
        public short worldY;
        public string sterilizator;

        public ExchangeMountSterilizeFromPaddockMessage()
        {
        }
        public ExchangeMountSterilizeFromPaddockMessage(string name,short worldX,short worldY,string sterilizator)
        {
            this.name = name;
            this.worldX = worldX;
            this.worldY = worldY;
            this.sterilizator = sterilizator;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)name);
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element worldX.");
            }

            writer.WriteShort((short)worldX);
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element worldY.");
            }

            writer.WriteShort((short)worldY);
            writer.WriteUTF((string)sterilizator);
        }
        public override void Deserialize(IDataReader reader)
        {
            name = (string)reader.ReadUTF();
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of ExchangeMountSterilizeFromPaddockMessage.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of ExchangeMountSterilizeFromPaddockMessage.worldY.");
            }

            sterilizator = (string)reader.ReadUTF();
        }


    }
}








