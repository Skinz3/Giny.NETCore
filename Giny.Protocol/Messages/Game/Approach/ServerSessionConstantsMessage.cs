using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ServerSessionConstantsMessage : NetworkMessage  
    { 
        public  const ushort Id = 8249;
        public override ushort MessageId => Id;

        public ServerSessionConstant[] variables;

        public ServerSessionConstantsMessage()
        {
        }
        public ServerSessionConstantsMessage(ServerSessionConstant[] variables)
        {
            this.variables = variables;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)variables.Length);
            for (uint _i1 = 0;_i1 < variables.Length;_i1++)
            {
                writer.WriteShort((short)(variables[_i1] as ServerSessionConstant).TypeId);
                (variables[_i1] as ServerSessionConstant).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            ServerSessionConstant _item1 = null;
            uint _variablesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _variablesLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<ServerSessionConstant>((short)_id1);
                _item1.Deserialize(reader);
                variables[_i1] = _item1;
            }

        }


    }
}








