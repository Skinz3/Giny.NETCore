using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicDateMessage : NetworkMessage  
    { 
        public  const ushort Id = 4573;
        public override ushort MessageId => Id;

        public byte day;
        public byte month;
        public short year;

        public BasicDateMessage()
        {
        }
        public BasicDateMessage(byte day,byte month,short year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (day < 0)
            {
                throw new System.Exception("Forbidden value (" + day + ") on element day.");
            }

            writer.WriteByte((byte)day);
            if (month < 0)
            {
                throw new System.Exception("Forbidden value (" + month + ") on element month.");
            }

            writer.WriteByte((byte)month);
            if (year < 0)
            {
                throw new System.Exception("Forbidden value (" + year + ") on element year.");
            }

            writer.WriteShort((short)year);
        }
        public override void Deserialize(IDataReader reader)
        {
            day = (byte)reader.ReadByte();
            if (day < 0)
            {
                throw new System.Exception("Forbidden value (" + day + ") on element of BasicDateMessage.day.");
            }

            month = (byte)reader.ReadByte();
            if (month < 0)
            {
                throw new System.Exception("Forbidden value (" + month + ") on element of BasicDateMessage.month.");
            }

            year = (short)reader.ReadShort();
            if (year < 0)
            {
                throw new System.Exception("Forbidden value (" + year + ") on element of BasicDateMessage.year.");
            }

        }


    }
}








