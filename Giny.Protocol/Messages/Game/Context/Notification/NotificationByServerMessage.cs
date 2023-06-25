using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class NotificationByServerMessage : NetworkMessage  
    { 
        public  const ushort Id = 9779;
        public override ushort MessageId => Id;

        public short id;
        public string[] parameters;
        public bool forceOpen;

        public NotificationByServerMessage()
        {
        }
        public NotificationByServerMessage(short id,string[] parameters,bool forceOpen)
        {
            this.id = id;
            this.parameters = parameters;
            this.forceOpen = forceOpen;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarShort((short)id);
            writer.WriteShort((short)parameters.Length);
            for (uint _i2 = 0;_i2 < parameters.Length;_i2++)
            {
                writer.WriteUTF((string)parameters[_i2]);
            }

            writer.WriteBoolean((bool)forceOpen);
        }
        public override void Deserialize(IDataReader reader)
        {
            string _val2 = null;
            id = (short)reader.ReadVarUhShort();
            if (id < 0)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of NotificationByServerMessage.id.");
            }

            uint _parametersLen = (uint)reader.ReadUShort();
            parameters = new string[_parametersLen];
            for (uint _i2 = 0;_i2 < _parametersLen;_i2++)
            {
                _val2 = (string)reader.ReadUTF();
                parameters[_i2] = (string)_val2;
            }

            forceOpen = (bool)reader.ReadBoolean();
        }


    }
}








