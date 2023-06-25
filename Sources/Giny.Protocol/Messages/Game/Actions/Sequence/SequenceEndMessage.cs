using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SequenceEndMessage : NetworkMessage  
    { 
        public  const ushort Id = 280;
        public override ushort MessageId => Id;

        public short actionId;
        public double authorId;
        public byte sequenceType;

        public SequenceEndMessage()
        {
        }
        public SequenceEndMessage(short actionId,double authorId,byte sequenceType)
        {
            this.actionId = actionId;
            this.authorId = authorId;
            this.sequenceType = sequenceType;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element actionId.");
            }

            writer.WriteVarShort((short)actionId);
            if (authorId < -9.00719925474099E+15 || authorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + authorId + ") on element authorId.");
            }

            writer.WriteDouble((double)authorId);
            writer.WriteByte((byte)sequenceType);
        }
        public override void Deserialize(IDataReader reader)
        {
            actionId = (short)reader.ReadVarUhShort();
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element of SequenceEndMessage.actionId.");
            }

            authorId = (double)reader.ReadDouble();
            if (authorId < -9.00719925474099E+15 || authorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + authorId + ") on element of SequenceEndMessage.authorId.");
            }

            sequenceType = (byte)reader.ReadByte();
        }


    }
}








