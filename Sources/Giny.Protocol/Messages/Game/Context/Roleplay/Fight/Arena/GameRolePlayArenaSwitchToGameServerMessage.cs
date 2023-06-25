using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaSwitchToGameServerMessage : NetworkMessage  
    { 
        public  const ushort Id = 9711;
        public override ushort MessageId => Id;

        public bool validToken;
        public byte[] ticket;
        public short homeServerId;

        public GameRolePlayArenaSwitchToGameServerMessage()
        {
        }
        public GameRolePlayArenaSwitchToGameServerMessage(bool validToken,byte[] ticket,short homeServerId)
        {
            this.validToken = validToken;
            this.ticket = ticket;
            this.homeServerId = homeServerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)validToken);
            writer.WriteVarInt((int)ticket.Length);
            for (uint _i2 = 0;_i2 < ticket.Length;_i2++)
            {
                writer.WriteByte((byte)ticket[_i2]);
            }

            writer.WriteShort((short)homeServerId);
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

            homeServerId = (short)reader.ReadShort();
        }


    }
}








