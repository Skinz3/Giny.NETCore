using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TreasureHuntGiveUpRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 4966;
        public override ushort MessageId => Id;

        public byte questType;

        public TreasureHuntGiveUpRequestMessage()
        {
        }
        public TreasureHuntGiveUpRequestMessage(byte questType)
        {
            this.questType = questType;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)questType);
        }
        public override void Deserialize(IDataReader reader)
        {
            questType = (byte)reader.ReadByte();
            if (questType < 0)
            {
                throw new System.Exception("Forbidden value (" + questType + ") on element of TreasureHuntGiveUpRequestMessage.questType.");
            }

        }


    }
}








