using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LivingObjectMessageRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 9662;
        public override ushort MessageId => Id;

        public short msgId;
        public string[] parameters;
        public int livingObject;

        public LivingObjectMessageRequestMessage()
        {
        }
        public LivingObjectMessageRequestMessage(short msgId,string[] parameters,int livingObject)
        {
            this.msgId = msgId;
            this.parameters = parameters;
            this.livingObject = livingObject;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (msgId < 0)
            {
                throw new System.Exception("Forbidden value (" + msgId + ") on element msgId.");
            }

            writer.WriteVarShort((short)msgId);
            writer.WriteShort((short)parameters.Length);
            for (uint _i2 = 0;_i2 < parameters.Length;_i2++)
            {
                writer.WriteUTF((string)parameters[_i2]);
            }

            if (livingObject < 0)
            {
                throw new System.Exception("Forbidden value (" + livingObject + ") on element livingObject.");
            }

            writer.WriteVarInt((int)livingObject);
        }
        public override void Deserialize(IDataReader reader)
        {
            string _val2 = null;
            msgId = (short)reader.ReadVarUhShort();
            if (msgId < 0)
            {
                throw new System.Exception("Forbidden value (" + msgId + ") on element of LivingObjectMessageRequestMessage.msgId.");
            }

            uint _parametersLen = (uint)reader.ReadUShort();
            parameters = new string[_parametersLen];
            for (uint _i2 = 0;_i2 < _parametersLen;_i2++)
            {
                _val2 = (string)reader.ReadUTF();
                parameters[_i2] = (string)_val2;
            }

            livingObject = (int)reader.ReadVarUhInt();
            if (livingObject < 0)
            {
                throw new System.Exception("Forbidden value (" + livingObject + ") on element of LivingObjectMessageRequestMessage.livingObject.");
            }

        }


    }
}








