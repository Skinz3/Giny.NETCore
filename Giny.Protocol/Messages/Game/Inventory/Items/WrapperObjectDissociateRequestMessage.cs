using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class WrapperObjectDissociateRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8460;
        public override ushort MessageId => Id;

        public int hostUID;
        public byte hostPos;

        public WrapperObjectDissociateRequestMessage()
        {
        }
        public WrapperObjectDissociateRequestMessage(int hostUID,byte hostPos)
        {
            this.hostUID = hostUID;
            this.hostPos = hostPos;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (hostUID < 0)
            {
                throw new System.Exception("Forbidden value (" + hostUID + ") on element hostUID.");
            }

            writer.WriteVarInt((int)hostUID);
            if (hostPos < 0 || hostPos > 255)
            {
                throw new System.Exception("Forbidden value (" + hostPos + ") on element hostPos.");
            }

            writer.WriteByte((byte)hostPos);
        }
        public override void Deserialize(IDataReader reader)
        {
            hostUID = (int)reader.ReadVarUhInt();
            if (hostUID < 0)
            {
                throw new System.Exception("Forbidden value (" + hostUID + ") on element of WrapperObjectDissociateRequestMessage.hostUID.");
            }

            hostPos = (byte)reader.ReadSByte();
            if (hostPos < 0 || hostPos > 255)
            {
                throw new System.Exception("Forbidden value (" + hostPos + ") on element of WrapperObjectDissociateRequestMessage.hostPos.");
            }

        }


    }
}








