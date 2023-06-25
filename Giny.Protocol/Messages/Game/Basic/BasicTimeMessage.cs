using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BasicTimeMessage : NetworkMessage  
    { 
        public  const ushort Id = 430;
        public override ushort MessageId => Id;

        public double timestamp;
        public short timezoneOffset;

        public BasicTimeMessage()
        {
        }
        public BasicTimeMessage(double timestamp,short timezoneOffset)
        {
            this.timestamp = timestamp;
            this.timezoneOffset = timezoneOffset;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (timestamp < 0 || timestamp > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + timestamp + ") on element timestamp.");
            }

            writer.WriteDouble((double)timestamp);
            writer.WriteShort((short)timezoneOffset);
        }
        public override void Deserialize(IDataReader reader)
        {
            timestamp = (double)reader.ReadDouble();
            if (timestamp < 0 || timestamp > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + timestamp + ") on element of BasicTimeMessage.timestamp.");
            }

            timezoneOffset = (short)reader.ReadShort();
        }


    }
}








