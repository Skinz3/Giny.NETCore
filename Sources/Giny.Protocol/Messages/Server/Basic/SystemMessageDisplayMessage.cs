using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SystemMessageDisplayMessage : NetworkMessage  
    { 
        public  const ushort Id = 5519;
        public override ushort MessageId => Id;

        public bool hangUp;
        public short msgId;
        public string[] parameters;

        public SystemMessageDisplayMessage()
        {
        }
        public SystemMessageDisplayMessage(bool hangUp,short msgId,string[] parameters)
        {
            this.hangUp = hangUp;
            this.msgId = msgId;
            this.parameters = parameters;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)hangUp);
            if (msgId < 0)
            {
                throw new System.Exception("Forbidden value (" + msgId + ") on element msgId.");
            }

            writer.WriteVarShort((short)msgId);
            writer.WriteShort((short)parameters.Length);
            for (uint _i3 = 0;_i3 < parameters.Length;_i3++)
            {
                writer.WriteUTF((string)parameters[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            string _val3 = null;
            hangUp = (bool)reader.ReadBoolean();
            msgId = (short)reader.ReadVarUhShort();
            if (msgId < 0)
            {
                throw new System.Exception("Forbidden value (" + msgId + ") on element of SystemMessageDisplayMessage.msgId.");
            }

            uint _parametersLen = (uint)reader.ReadUShort();
            parameters = new string[_parametersLen];
            for (uint _i3 = 0;_i3 < _parametersLen;_i3++)
            {
                _val3 = (string)reader.ReadUTF();
                parameters[_i3] = (string)_val3;
            }

        }


    }
}








