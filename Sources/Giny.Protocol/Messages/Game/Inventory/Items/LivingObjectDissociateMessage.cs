using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LivingObjectDissociateMessage : NetworkMessage  
    { 
        public  const ushort Id = 8132;
        public override ushort MessageId => Id;

        public int livingUID;
        public byte livingPosition;

        public LivingObjectDissociateMessage()
        {
        }
        public LivingObjectDissociateMessage(int livingUID,byte livingPosition)
        {
            this.livingUID = livingUID;
            this.livingPosition = livingPosition;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (livingUID < 0)
            {
                throw new System.Exception("Forbidden value (" + livingUID + ") on element livingUID.");
            }

            writer.WriteVarInt((int)livingUID);
            if (livingPosition < 0 || livingPosition > 255)
            {
                throw new System.Exception("Forbidden value (" + livingPosition + ") on element livingPosition.");
            }

            writer.WriteByte((byte)livingPosition);
        }
        public override void Deserialize(IDataReader reader)
        {
            livingUID = (int)reader.ReadVarUhInt();
            if (livingUID < 0)
            {
                throw new System.Exception("Forbidden value (" + livingUID + ") on element of LivingObjectDissociateMessage.livingUID.");
            }

            livingPosition = (byte)reader.ReadSByte();
            if (livingPosition < 0 || livingPosition > 255)
            {
                throw new System.Exception("Forbidden value (" + livingPosition + ") on element of LivingObjectDissociateMessage.livingPosition.");
            }

        }


    }
}








