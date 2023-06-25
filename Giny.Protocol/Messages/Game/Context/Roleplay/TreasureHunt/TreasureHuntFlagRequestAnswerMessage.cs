using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TreasureHuntFlagRequestAnswerMessage : NetworkMessage  
    { 
        public  const ushort Id = 309;
        public override ushort MessageId => Id;

        public byte questType;
        public byte result;
        public byte index;

        public TreasureHuntFlagRequestAnswerMessage()
        {
        }
        public TreasureHuntFlagRequestAnswerMessage(byte questType,byte result,byte index)
        {
            this.questType = questType;
            this.result = result;
            this.index = index;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)questType);
            writer.WriteByte((byte)result);
            if (index < 0)
            {
                throw new System.Exception("Forbidden value (" + index + ") on element index.");
            }

            writer.WriteByte((byte)index);
        }
        public override void Deserialize(IDataReader reader)
        {
            questType = (byte)reader.ReadByte();
            if (questType < 0)
            {
                throw new System.Exception("Forbidden value (" + questType + ") on element of TreasureHuntFlagRequestAnswerMessage.questType.");
            }

            result = (byte)reader.ReadByte();
            if (result < 0)
            {
                throw new System.Exception("Forbidden value (" + result + ") on element of TreasureHuntFlagRequestAnswerMessage.result.");
            }

            index = (byte)reader.ReadByte();
            if (index < 0)
            {
                throw new System.Exception("Forbidden value (" + index + ") on element of TreasureHuntFlagRequestAnswerMessage.index.");
            }

        }


    }
}








