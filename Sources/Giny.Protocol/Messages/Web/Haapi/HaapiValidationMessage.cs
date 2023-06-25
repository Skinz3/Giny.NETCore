using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiValidationMessage : NetworkMessage  
    { 
        public  const ushort Id = 1415;
        public override ushort MessageId => Id;

        public byte action;
        public byte code;

        public HaapiValidationMessage()
        {
        }
        public HaapiValidationMessage(byte action,byte code)
        {
            this.action = action;
            this.code = code;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)action);
            writer.WriteByte((byte)code);
        }
        public override void Deserialize(IDataReader reader)
        {
            action = (byte)reader.ReadByte();
            if (action < 0)
            {
                throw new System.Exception("Forbidden value (" + action + ") on element of HaapiValidationMessage.action.");
            }

            code = (byte)reader.ReadByte();
            if (code < 0)
            {
                throw new System.Exception("Forbidden value (" + code + ") on element of HaapiValidationMessage.code.");
            }

        }


    }
}








