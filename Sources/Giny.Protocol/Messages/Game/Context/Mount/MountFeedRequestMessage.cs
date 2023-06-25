using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MountFeedRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 9085;
        public override ushort MessageId => Id;

        public int mountUid;
        public byte mountLocation;
        public int mountFoodUid;
        public int quantity;

        public MountFeedRequestMessage()
        {
        }
        public MountFeedRequestMessage(int mountUid,byte mountLocation,int mountFoodUid,int quantity)
        {
            this.mountUid = mountUid;
            this.mountLocation = mountLocation;
            this.mountFoodUid = mountFoodUid;
            this.quantity = quantity;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (mountUid < 0)
            {
                throw new System.Exception("Forbidden value (" + mountUid + ") on element mountUid.");
            }

            writer.WriteVarInt((int)mountUid);
            writer.WriteByte((byte)mountLocation);
            if (mountFoodUid < 0)
            {
                throw new System.Exception("Forbidden value (" + mountFoodUid + ") on element mountFoodUid.");
            }

            writer.WriteVarInt((int)mountFoodUid);
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteVarInt((int)quantity);
        }
        public override void Deserialize(IDataReader reader)
        {
            mountUid = (int)reader.ReadVarUhInt();
            if (mountUid < 0)
            {
                throw new System.Exception("Forbidden value (" + mountUid + ") on element of MountFeedRequestMessage.mountUid.");
            }

            mountLocation = (byte)reader.ReadByte();
            mountFoodUid = (int)reader.ReadVarUhInt();
            if (mountFoodUid < 0)
            {
                throw new System.Exception("Forbidden value (" + mountFoodUid + ") on element of MountFeedRequestMessage.mountFoodUid.");
            }

            quantity = (int)reader.ReadVarUhInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of MountFeedRequestMessage.quantity.");
            }

        }


    }
}








