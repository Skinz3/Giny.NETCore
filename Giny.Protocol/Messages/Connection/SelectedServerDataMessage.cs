using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SelectedServerDataMessage : NetworkMessage  
    { 
        public  const ushort Id = 9579;
        public override ushort MessageId => Id;

        public short serverId;
        public string address;
        public short[] ports;
        public bool canCreateNewCharacter;
        public byte[] ticket;

        public SelectedServerDataMessage()
        {
        }
        public SelectedServerDataMessage(short serverId,string address,short[] ports,bool canCreateNewCharacter,byte[] ticket)
        {
            this.serverId = serverId;
            this.address = address;
            this.ports = ports;
            this.canCreateNewCharacter = canCreateNewCharacter;
            this.ticket = ticket;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (serverId < 0)
            {
                throw new System.Exception("Forbidden value (" + serverId + ") on element serverId.");
            }

            writer.WriteVarShort((short)serverId);
            writer.WriteUTF((string)address);
            writer.WriteShort((short)ports.Length);
            for (uint _i3 = 0;_i3 < ports.Length;_i3++)
            {
                if (ports[_i3] < 0)
                {
                    throw new System.Exception("Forbidden value (" + ports[_i3] + ") on element 3 (starting at 1) of ports.");
                }

                writer.WriteVarShort((short)ports[_i3]);
            }

            writer.WriteBoolean((bool)canCreateNewCharacter);
            writer.WriteVarInt((int)ticket.Length);
            for (uint _i5 = 0;_i5 < ticket.Length;_i5++)
            {
                writer.WriteByte((byte)ticket[_i5]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val3 = 0;
            int _val5 = 0;
            serverId = (short)reader.ReadVarUhShort();
            if (serverId < 0)
            {
                throw new System.Exception("Forbidden value (" + serverId + ") on element of SelectedServerDataMessage.serverId.");
            }

            address = (string)reader.ReadUTF();
            uint _portsLen = (uint)reader.ReadUShort();
            ports = new short[_portsLen];
            for (uint _i3 = 0;_i3 < _portsLen;_i3++)
            {
                _val3 = (uint)reader.ReadVarUhShort();
                if (_val3 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of ports.");
                }

                ports[_i3] = (short)_val3;
            }

            canCreateNewCharacter = (bool)reader.ReadBoolean();
            uint _ticketLen = (uint)reader.ReadVarInt();
            ticket = new byte[_ticketLen];
            for (uint _i5 = 0;_i5 < _ticketLen;_i5++)
            {
                _val5 = (int)reader.ReadByte();
                ticket[_i5] = (byte)_val5;
            }

        }


    }
}








