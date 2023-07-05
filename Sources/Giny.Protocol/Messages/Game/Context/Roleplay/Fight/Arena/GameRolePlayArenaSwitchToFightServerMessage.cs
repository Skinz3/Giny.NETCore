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
        public const ushort Id = 1936;
        public override ushort MessageId => Id;

        public string address;
        public short[] ports;
        public string token;

        public GameRolePlayArenaSwitchToFightServerMessage()
        {
        }
        public GameRolePlayArenaSwitchToFightServerMessage(string address, short[] ports, string token)
        {
            this.address = address;
            this.ports = ports;
            this.token = token;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)address);
            writer.WriteShort((short)ports.Length);
            for (uint _i2 = 0; _i2 < ports.Length; _i2++)
            {
                if (ports[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + ports[_i2] + ") on element 2 (starting at 1) of ports.");
                }

                writer.WriteVarShort((short)ports[_i2]);
            }

            writer.WriteUTF((string)token);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            address = (string)reader.ReadUTF();
            uint _portsLen = (uint)reader.ReadUShort();
            ports = new short[_portsLen];
            for (uint _i2 = 0; _i2 < _portsLen; _i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of ports.");
                }

                ports[_i2] = (short)_val2;
            }

            token = (string)reader.ReadUTF();
        }

    }
}


