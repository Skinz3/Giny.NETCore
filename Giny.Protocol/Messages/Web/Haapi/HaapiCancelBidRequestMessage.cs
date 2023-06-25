using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HaapiCancelBidRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 5316;
        public override ushort MessageId => Id;

        public long id;
        public byte type;

        public HaapiCancelBidRequestMessage()
        {
        }
        public HaapiCancelBidRequestMessage(long id,byte type)
        {
            this.id = id;
            this.type = type;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteVarLong((long)id);
            writer.WriteByte((byte)type);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (long)reader.ReadVarUhLong();
            if (id < 0 || id > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of HaapiCancelBidRequestMessage.id.");
            }

            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of HaapiCancelBidRequestMessage.type.");
            }

        }


    }
}








