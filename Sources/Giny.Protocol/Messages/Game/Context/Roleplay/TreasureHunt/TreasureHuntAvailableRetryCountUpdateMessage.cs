using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TreasureHuntAvailableRetryCountUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 5909;
        public override ushort MessageId => Id;

        public byte questType;
        public int availableRetryCount;

        public TreasureHuntAvailableRetryCountUpdateMessage()
        {
        }
        public TreasureHuntAvailableRetryCountUpdateMessage(byte questType,int availableRetryCount)
        {
            this.questType = questType;
            this.availableRetryCount = availableRetryCount;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)questType);
            writer.WriteInt((int)availableRetryCount);
        }
        public override void Deserialize(IDataReader reader)
        {
            questType = (byte)reader.ReadByte();
            if (questType < 0)
            {
                throw new System.Exception("Forbidden value (" + questType + ") on element of TreasureHuntAvailableRetryCountUpdateMessage.questType.");
            }

            availableRetryCount = (int)reader.ReadInt();
        }


    }
}








