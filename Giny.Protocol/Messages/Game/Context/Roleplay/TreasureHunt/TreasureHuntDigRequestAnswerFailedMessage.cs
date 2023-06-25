using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TreasureHuntDigRequestAnswerFailedMessage : TreasureHuntDigRequestAnswerMessage  
    { 
        public new const ushort Id = 3869;
        public override ushort MessageId => Id;

        public byte wrongFlagCount;

        public TreasureHuntDigRequestAnswerFailedMessage()
        {
        }
        public TreasureHuntDigRequestAnswerFailedMessage(byte wrongFlagCount,byte questType,byte result)
        {
            this.wrongFlagCount = wrongFlagCount;
            this.questType = questType;
            this.result = result;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (wrongFlagCount < 0)
            {
                throw new System.Exception("Forbidden value (" + wrongFlagCount + ") on element wrongFlagCount.");
            }

            writer.WriteByte((byte)wrongFlagCount);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            wrongFlagCount = (byte)reader.ReadByte();
            if (wrongFlagCount < 0)
            {
                throw new System.Exception("Forbidden value (" + wrongFlagCount + ") on element of TreasureHuntDigRequestAnswerFailedMessage.wrongFlagCount.");
            }

        }


    }
}








