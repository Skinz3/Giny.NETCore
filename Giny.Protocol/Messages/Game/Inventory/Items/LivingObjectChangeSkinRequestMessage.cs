using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LivingObjectChangeSkinRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 908;
        public override ushort MessageId => Id;

        public int livingUID;
        public byte livingPosition;
        public int skinId;

        public LivingObjectChangeSkinRequestMessage()
        {
        }
        public LivingObjectChangeSkinRequestMessage(int livingUID,byte livingPosition,int skinId)
        {
            this.livingUID = livingUID;
            this.livingPosition = livingPosition;
            this.skinId = skinId;
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
            if (skinId < 0)
            {
                throw new System.Exception("Forbidden value (" + skinId + ") on element skinId.");
            }

            writer.WriteVarInt((int)skinId);
        }
        public override void Deserialize(IDataReader reader)
        {
            livingUID = (int)reader.ReadVarUhInt();
            if (livingUID < 0)
            {
                throw new System.Exception("Forbidden value (" + livingUID + ") on element of LivingObjectChangeSkinRequestMessage.livingUID.");
            }

            livingPosition = (byte)reader.ReadSByte();
            if (livingPosition < 0 || livingPosition > 255)
            {
                throw new System.Exception("Forbidden value (" + livingPosition + ") on element of LivingObjectChangeSkinRequestMessage.livingPosition.");
            }

            skinId = (int)reader.ReadVarUhInt();
            if (skinId < 0)
            {
                throw new System.Exception("Forbidden value (" + skinId + ") on element of LivingObjectChangeSkinRequestMessage.skinId.");
            }

        }


    }
}








