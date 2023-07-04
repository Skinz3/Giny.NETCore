using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceApplicationIsAnsweredMessage : NetworkMessage
    {
        public const ushort Id = 1445;
        public override ushort MessageId => Id;

        public bool accepted;
        public AllianceInformation allianceInformation;

        public AllianceApplicationIsAnsweredMessage()
        {
        }
        public AllianceApplicationIsAnsweredMessage(bool accepted, AllianceInformation allianceInformation)
        {
            this.accepted = accepted;
            this.allianceInformation = allianceInformation;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)accepted);
            allianceInformation.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            accepted = (bool)reader.ReadBoolean();
            allianceInformation = new AllianceInformation();
            allianceInformation.Deserialize(reader);
        }

    }
}


