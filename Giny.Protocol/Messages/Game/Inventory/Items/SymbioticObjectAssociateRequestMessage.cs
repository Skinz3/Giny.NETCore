using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SymbioticObjectAssociateRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 3670;
        public override ushort MessageId => Id;

        public int symbioteUID;
        public byte symbiotePos;
        public int hostUID;
        public byte hostPos;

        public SymbioticObjectAssociateRequestMessage()
        {
        }
        public SymbioticObjectAssociateRequestMessage(int symbioteUID,byte symbiotePos,int hostUID,byte hostPos)
        {
            this.symbioteUID = symbioteUID;
            this.symbiotePos = symbiotePos;
            this.hostUID = hostUID;
            this.hostPos = hostPos;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (symbioteUID < 0)
            {
                throw new System.Exception("Forbidden value (" + symbioteUID + ") on element symbioteUID.");
            }

            writer.WriteVarInt((int)symbioteUID);
            if (symbiotePos < 0 || symbiotePos > 255)
            {
                throw new System.Exception("Forbidden value (" + symbiotePos + ") on element symbiotePos.");
            }

            writer.WriteByte((byte)symbiotePos);
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
            symbioteUID = (int)reader.ReadVarUhInt();
            if (symbioteUID < 0)
            {
                throw new System.Exception("Forbidden value (" + symbioteUID + ") on element of SymbioticObjectAssociateRequestMessage.symbioteUID.");
            }

            symbiotePos = (byte)reader.ReadSByte();
            if (symbiotePos < 0 || symbiotePos > 255)
            {
                throw new System.Exception("Forbidden value (" + symbiotePos + ") on element of SymbioticObjectAssociateRequestMessage.symbiotePos.");
            }

            hostUID = (int)reader.ReadVarUhInt();
            if (hostUID < 0)
            {
                throw new System.Exception("Forbidden value (" + hostUID + ") on element of SymbioticObjectAssociateRequestMessage.hostUID.");
            }

            hostPos = (byte)reader.ReadSByte();
            if (hostPos < 0 || hostPos > 255)
            {
                throw new System.Exception("Forbidden value (" + hostPos + ") on element of SymbioticObjectAssociateRequestMessage.hostPos.");
            }

        }


    }
}








