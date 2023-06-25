using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ReloginTokenStatusMessage : NetworkMessage  
    { 
        public  const ushort Id = 6325;
        public override ushort MessageId => Id;

        public bool validToken;
        public byte[] ticket;

        public ReloginTokenStatusMessage()
        {
        }
        public ReloginTokenStatusMessage(bool validToken,byte[] ticket)
        {
            this.validToken = validToken;
            this.ticket = ticket;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)validToken);
            writer.WriteVarInt((int)ticket.Length);
            for (uint _i2 = 0;_i2 < ticket.Length;_i2++)
            {
                writer.WriteByte((byte)ticket[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val2 = 0;
            validToken = (bool)reader.ReadBoolean();
            uint _ticketLen = (uint)reader.ReadVarInt();
            ticket = new byte[_ticketLen];
            for (uint _i2 = 0;_i2 < _ticketLen;_i2++)
            {
                _val2 = (int)reader.ReadByte();
                ticket[_i2] = (byte)_val2;
            }

        }


    }
}








