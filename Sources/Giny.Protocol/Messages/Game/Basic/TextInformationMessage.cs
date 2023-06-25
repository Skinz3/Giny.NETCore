using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TextInformationMessage : NetworkMessage  
    { 
        public  const ushort Id = 6875;
        public override ushort MessageId => Id;

        public byte msgType;
        public short msgId;
        public string[] parameters;

        public TextInformationMessage()
        {
        }
        public TextInformationMessage(byte msgType,short msgId,string[] parameters)
        {
            this.msgType = msgType;
            this.msgId = msgId;
            this.parameters = parameters;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)msgType);
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
            msgType = (byte)reader.ReadByte();
            if (msgType < 0)
            {
                throw new System.Exception("Forbidden value (" + msgType + ") on element of TextInformationMessage.msgType.");
            }

            msgId = (short)reader.ReadVarUhShort();
            if (msgId < 0)
            {
                throw new System.Exception("Forbidden value (" + msgId + ") on element of TextInformationMessage.msgId.");
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








