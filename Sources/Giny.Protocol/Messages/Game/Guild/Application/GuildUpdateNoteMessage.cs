using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildUpdateNoteMessage : NetworkMessage  
    { 
        public  const ushort Id = 5967;
        public override ushort MessageId => Id;

        public long memberId;
        public string note;

        public GuildUpdateNoteMessage()
        {
        }
        public GuildUpdateNoteMessage(long memberId,string note)
        {
            this.memberId = memberId;
            this.note = note;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            writer.WriteUTF((string)note);
        }
        public override void Deserialize(IDataReader reader)
        {
            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of GuildUpdateNoteMessage.memberId.");
            }

            note = (string)reader.ReadUTF();
        }


    }
}








