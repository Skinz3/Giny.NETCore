using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EntityTalkMessage : NetworkMessage  
    { 
        public  const ushort Id = 4154;
        public override ushort MessageId => Id;

        public double entityId;
        public short textId;
        public string[] parameters;

        public EntityTalkMessage()
        {
        }
        public EntityTalkMessage(double entityId,short textId,string[] parameters)
        {
            this.entityId = entityId;
            this.textId = textId;
            this.parameters = parameters;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (entityId < -9.00719925474099E+15 || entityId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + entityId + ") on element entityId.");
            }

            writer.WriteDouble((double)entityId);
            if (textId < 0)
            {
                throw new System.Exception("Forbidden value (" + textId + ") on element textId.");
            }

            writer.WriteVarShort((short)textId);
            writer.WriteShort((short)parameters.Length);
            for (uint _i3 = 0;_i3 < parameters.Length;_i3++)
            {
                writer.WriteUTF((string)parameters[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            string _val3 = null;
            entityId = (double)reader.ReadDouble();
            if (entityId < -9.00719925474099E+15 || entityId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + entityId + ") on element of EntityTalkMessage.entityId.");
            }

            textId = (short)reader.ReadVarUhShort();
            if (textId < 0)
            {
                throw new System.Exception("Forbidden value (" + textId + ") on element of EntityTalkMessage.textId.");
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








