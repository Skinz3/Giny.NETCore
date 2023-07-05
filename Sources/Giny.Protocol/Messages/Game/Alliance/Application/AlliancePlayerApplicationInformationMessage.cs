using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AlliancePlayerApplicationInformationMessage : AlliancePlayerApplicationAbstractMessage
    {
        public new const ushort Id = 96;
        public override ushort MessageId => Id;

        public AllianceInformation allianceInformation;
        public SocialApplicationInformation apply;

        public AlliancePlayerApplicationInformationMessage()
        {
        }
        public AlliancePlayerApplicationInformationMessage(AllianceInformation allianceInformation, SocialApplicationInformation apply)
        {
            this.allianceInformation = allianceInformation;
            this.apply = apply;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInformation.Serialize(writer);
            apply.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInformation = new AllianceInformation();
            allianceInformation.Deserialize(reader);
            apply = new SocialApplicationInformation();
            apply.Deserialize(reader);
        }

    }
}


