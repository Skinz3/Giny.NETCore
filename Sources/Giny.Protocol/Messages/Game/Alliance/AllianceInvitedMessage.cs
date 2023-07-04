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
        public const ushort Id = 8538;
        public override ushort MessageId => Id;

        public string recruterName;
        public AllianceInformation allianceInfo;

        public AllianceInvitedMessage()
        {
        }
        public AllianceInvitedMessage(string recruterName, AllianceInformation allianceInfo)
        {
            this.recruterName = recruterName;
            this.allianceInfo = allianceInfo;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)recruterName);
            allianceInfo.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            recruterName = (string)reader.ReadUTF();
            allianceInfo = new AllianceInformation();
            allianceInfo.Deserialize(reader);
        }

    }
}


