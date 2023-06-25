using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicAckMessage : NetworkMessage  
    { 
        public  const ushort Id = 8655;
        public override ushort MessageId => Id;

        public int seq;
        public short lastPacketId;

        public BasicAckMessage()
        {
        }
        public BasicAckMessage(int seq,short lastPacketId)
        {
            this.seq = seq;
            this.lastPacketId = lastPacketId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (seq < 0)
            {
                throw new System.Exception("Forbidden value (" + seq + ") on element seq.");
            }

            writer.WriteVarInt((int)seq);
            if (lastPacketId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastPacketId + ") on element lastPacketId.");
            }

            writer.WriteVarShort((short)lastPacketId);
        }
        public override void Deserialize(IDataReader reader)
        {
            seq = (int)reader.ReadVarUhInt();
            if (seq < 0)
            {
                throw new System.Exception("Forbidden value (" + seq + ") on element of BasicAckMessage.seq.");
            }

            lastPacketId = (short)reader.ReadVarUhShort();
            if (lastPacketId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastPacketId + ") on element of BasicAckMessage.lastPacketId.");
            }

        }


    }
}








