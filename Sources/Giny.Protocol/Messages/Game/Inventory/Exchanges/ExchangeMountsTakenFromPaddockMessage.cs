using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ExchangeMountsTakenFromPaddockMessage : NetworkMessage  
    { 
        public  const ushort Id = 836;
        public override ushort MessageId => Id;

        public string name;
        public short worldX;
        public short worldY;
        public string ownername;

        public ExchangeMountsTakenFromPaddockMessage()
        {
        }
        public ExchangeMountsTakenFromPaddockMessage(string name,short worldX,short worldY,string ownername)
        {
            this.name = name;
            this.worldX = worldX;
            this.worldY = worldY;
            this.ownername = ownername;
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
            writer.WriteUTF((string)ownername);
        }
        public override void Deserialize(IDataReader reader)
        {
            name = (string)reader.ReadUTF();
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of ExchangeMountsTakenFromPaddockMessage.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of ExchangeMountsTakenFromPaddockMessage.worldY.");
            }

            ownername = (string)reader.ReadUTF();
        }


    }
}








