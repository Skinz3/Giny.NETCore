using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MoodSmileyResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 6863;
        public override ushort MessageId => Id;

        public byte resultCode;
        public short smileyId;

        public MoodSmileyResultMessage()
        {
        }
        public MoodSmileyResultMessage(byte resultCode,short smileyId)
        {
            this.resultCode = resultCode;
            this.smileyId = smileyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)resultCode);
            if (smileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + smileyId + ") on element smileyId.");
            }

            writer.WriteVarShort((short)smileyId);
        }
        public override void Deserialize(IDataReader reader)
        {
            resultCode = (byte)reader.ReadByte();
            if (resultCode < 0)
            {
                throw new System.Exception("Forbidden value (" + resultCode + ") on element of MoodSmileyResultMessage.resultCode.");
            }

            smileyId = (short)reader.ReadVarUhShort();
            if (smileyId < 0)
            {
                throw new System.Exception("Forbidden value (" + smileyId + ") on element of MoodSmileyResultMessage.smileyId.");
            }

        }


    }
}








