using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class TaxCollectorAttackedResultMessage : NetworkMessage
    {
        public const ushort Id = 7440;
        public override ushort MessageId => Id;

        public bool deadOrAlive;
        public TaxCollectorBasicInformations basicInfos;
        public BasicAllianceInformations alliance;

        public TaxCollectorAttackedResultMessage()
        {
        }
        public TaxCollectorAttackedResultMessage(bool deadOrAlive, TaxCollectorBasicInformations basicInfos, BasicAllianceInformations alliance)
        {
            this.deadOrAlive = deadOrAlive;
            this.basicInfos = basicInfos;
            this.alliance = alliance;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)deadOrAlive);
            basicInfos.Serialize(writer);
            alliance.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            deadOrAlive = (bool)reader.ReadBoolean();
            basicInfos = new TaxCollectorBasicInformations();
            basicInfos.Deserialize(reader);
            alliance = new BasicAllianceInformations();
            alliance.Deserialize(reader);
        }

    }
}


