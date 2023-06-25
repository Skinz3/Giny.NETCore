using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AlmanachCalendarDateMessage : NetworkMessage  
    { 
        public  const ushort Id = 8752;
        public override ushort MessageId => Id;

        public int date;

        public AlmanachCalendarDateMessage()
        {
        }
        public AlmanachCalendarDateMessage(int date)
        {
            this.date = date;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt((int)date);
        }
        public override void Deserialize(IDataReader reader)
        {
            date = (int)reader.ReadInt();
        }


    }
}








