using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceFightStartedMessage : NetworkMessage
    {
        public const ushort Id = 9908;
        public override ushort MessageId => Id;

        public SocialFightInfo allianceFightInfo;
        public FightPhase phase;

        public AllianceFightStartedMessage()
        {
        }
        public AllianceFightStartedMessage(SocialFightInfo allianceFightInfo, FightPhase phase)
        {
            this.allianceFightInfo = allianceFightInfo;
            this.phase = phase;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceFightInfo.Serialize(writer);
            phase.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceFightInfo = new SocialFightInfo();
            allianceFightInfo.Deserialize(reader);
            phase = new FightPhase();
            phase.Deserialize(reader);
        }

    }
}


