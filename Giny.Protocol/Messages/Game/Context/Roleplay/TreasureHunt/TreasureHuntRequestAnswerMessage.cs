using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TreasureHuntRequestAnswerMessage : NetworkMessage  
    { 
        public  const ushort Id = 6318;
        public override ushort MessageId => Id;

        public byte questType;
        public byte result;

        public TreasureHuntRequestAnswerMessage()
        {
        }
        public TreasureHuntRequestAnswerMessage(byte questType,byte result)
        {
            this.questType = questType;
            this.result = result;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)questType);
            writer.WriteByte((byte)result);
        }
        public override void Deserialize(IDataReader reader)
        {
            questType = (byte)reader.ReadByte();
            if (questType < 0)
            {
                throw new System.Exception("Forbidden value (" + questType + ") on element of TreasureHuntRequestAnswerMessage.questType.");
            }

            result = (byte)reader.ReadByte();
            if (result < 0)
            {
                throw new System.Exception("Forbidden value (" + result + ") on element of TreasureHuntRequestAnswerMessage.result.");
            }

        }


    }
}








