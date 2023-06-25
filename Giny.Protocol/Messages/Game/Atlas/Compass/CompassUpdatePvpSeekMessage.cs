using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CompassUpdatePvpSeekMessage : CompassUpdateMessage  
    { 
        public new const ushort Id = 8299;
        public override ushort MessageId => Id;

        public long memberId;
        public string memberName;

        public CompassUpdatePvpSeekMessage()
        {
        }
        public CompassUpdatePvpSeekMessage(long memberId,string memberName,byte type,MapCoordinates coords)
        {
            this.memberId = memberId;
            this.memberName = memberName;
            this.type = type;
            this.coords = coords;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element memberId.");
            }

            writer.WriteVarLong((long)memberId);
            writer.WriteUTF((string)memberName);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            memberId = (long)reader.ReadVarUhLong();
            if (memberId < 0 || memberId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + memberId + ") on element of CompassUpdatePvpSeekMessage.memberId.");
            }

            memberName = (string)reader.ReadUTF();
        }


    }
}








