using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AccountLoggingKickedMessage : NetworkMessage  
    { 
        public  const ushort Id = 3647;
        public override ushort MessageId => Id;

        public short days;
        public byte hours;
        public byte minutes;

        public AccountLoggingKickedMessage()
        {
        }
        public AccountLoggingKickedMessage(short days,byte hours,byte minutes)
        {
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (days < 0)
            {
                throw new System.Exception("Forbidden value (" + days + ") on element days.");
            }

            writer.WriteVarShort((short)days);
            if (hours < 0)
            {
                throw new System.Exception("Forbidden value (" + hours + ") on element hours.");
            }

            writer.WriteByte((byte)hours);
            if (minutes < 0)
            {
                throw new System.Exception("Forbidden value (" + minutes + ") on element minutes.");
            }

            writer.WriteByte((byte)minutes);
        }
        public override void Deserialize(IDataReader reader)
        {
            days = (short)reader.ReadVarUhShort();
            if (days < 0)
            {
                throw new System.Exception("Forbidden value (" + days + ") on element of AccountLoggingKickedMessage.days.");
            }

            hours = (byte)reader.ReadByte();
            if (hours < 0)
            {
                throw new System.Exception("Forbidden value (" + hours + ") on element of AccountLoggingKickedMessage.hours.");
            }

            minutes = (byte)reader.ReadByte();
            if (minutes < 0)
            {
                throw new System.Exception("Forbidden value (" + minutes + ") on element of AccountLoggingKickedMessage.minutes.");
            }

        }


    }
}








