using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PaginationRequestAbstractMessage : NetworkMessage  
    { 
        public  const ushort Id = 3535;
        public override ushort MessageId => Id;

        public double offset;
        public uint count;

        public PaginationRequestAbstractMessage()
        {
        }
        public PaginationRequestAbstractMessage(double offset,uint count)
        {
            this.offset = offset;
            this.count = count;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (offset < 0 || offset > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + offset + ") on element offset.");
            }

            writer.WriteDouble((double)offset);
            if (count < 0 || count > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + count + ") on element count.");
            }

            writer.WriteUInt((uint)count);
        }
        public override void Deserialize(IDataReader reader)
        {
            offset = (double)reader.ReadDouble();
            if (offset < 0 || offset > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + offset + ") on element of PaginationRequestAbstractMessage.offset.");
            }

            count = (uint)reader.ReadUInt();
            if (count < 0 || count > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + count + ") on element of PaginationRequestAbstractMessage.count.");
            }

        }


    }
}








