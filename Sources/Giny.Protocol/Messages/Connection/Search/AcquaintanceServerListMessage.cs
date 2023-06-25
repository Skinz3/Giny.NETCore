using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AcquaintanceServerListMessage : NetworkMessage  
    { 
        public  const ushort Id = 2372;
        public override ushort MessageId => Id;

        public short[] servers;

        public AcquaintanceServerListMessage()
        {
        }
        public AcquaintanceServerListMessage(short[] servers)
        {
            this.servers = servers;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)servers.Length);
            for (uint _i1 = 0;_i1 < servers.Length;_i1++)
            {
                if (servers[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + servers[_i1] + ") on element 1 (starting at 1) of servers.");
                }

                writer.WriteVarShort((short)servers[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _serversLen = (uint)reader.ReadUShort();
            servers = new short[_serversLen];
            for (uint _i1 = 0;_i1 < _serversLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of servers.");
                }

                servers[_i1] = (short)_val1;
            }

        }


    }
}








