using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceInvitedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4726;
        public override ushort MessageId => Id;

        public long recruterId;
        public string recruterName;
        public BasicNamedAllianceInformations allianceInfo;

        public AllianceInvitedMessage()
        {
        }
        public AllianceInvitedMessage(long recruterId,string recruterName,BasicNamedAllianceInformations allianceInfo)
        {
            this.recruterId = recruterId;
            this.recruterName = recruterName;
            this.allianceInfo = allianceInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (recruterId < 0 || recruterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + recruterId + ") on element recruterId.");
            }

            writer.WriteVarLong((long)recruterId);
            writer.WriteUTF((string)recruterName);
            allianceInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            recruterId = (long)reader.ReadVarUhLong();
            if (recruterId < 0 || recruterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + recruterId + ") on element of AllianceInvitedMessage.recruterId.");
            }

            recruterName = (string)reader.ReadUTF();
            allianceInfo = new BasicNamedAllianceInformations();
            allianceInfo.Deserialize(reader);
        }


    }
}








