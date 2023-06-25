using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayArenaSwitchToFightServerMessage : NetworkMessage  
    { 
        public  const ushort Id = 3780;
        public override ushort MessageId => Id;

        public string address;
        public short[] ports;
        public byte[] ticket;

        public GameRolePlayArenaSwitchToFightServerMessage()
        {
        }
        public GameRolePlayArenaSwitchToFightServerMessage(string address,short[] ports,byte[] ticket)
        {
            this.address = address;
            this.ports = ports;
            this.ticket = ticket;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)address);
            writer.WriteShort((short)ports.Length);
            for (uint _i2 = 0;_i2 < ports.Length;_i2++)
            {
                if (ports[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + ports[_i2] + ") on element 2 (starting at 1) of ports.");
                }

                writer.WriteVarShort((short)ports[_i2]);
            }

            writer.WriteVarInt((int)ticket.Length);
            for (uint _i3 = 0;_i3 < ticket.Length;_i3++)
            {
                writer.WriteByte((byte)ticket[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            int _val3 = 0;
            address = (string)reader.ReadUTF();
            uint _portsLen = (uint)reader.ReadUShort();
            ports = new short[_portsLen];
            for (uint _i2 = 0;_i2 < _portsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of ports.");
                }

                ports[_i2] = (short)_val2;
            }

            uint _ticketLen = (uint)reader.ReadVarInt();
            ticket = new byte[_ticketLen];
            for (uint _i3 = 0;_i3 < _ticketLen;_i3++)
            {
                _val3 = (int)reader.ReadByte();
                ticket[_i3] = (byte)_val3;
            }

        }


    }
}








