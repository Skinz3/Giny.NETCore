using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceFightPhaseUpdateMessage : NetworkMessage
    {
        public const ushort Id = 460;
        public override ushort MessageId => Id;

        public SocialFightInfo allianceFightInfo;
        public FightPhase newPhase;

        public AllianceFightPhaseUpdateMessage()
        {
        }
        public AllianceFightPhaseUpdateMessage(SocialFightInfo allianceFightInfo, FightPhase newPhase)
        {
            this.allianceFightInfo = allianceFightInfo;
            this.newPhase = newPhase;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceFightInfo.Serialize(writer);
            newPhase.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceFightInfo = new SocialFightInfo();
            allianceFightInfo.Deserialize(reader);
            newPhase = new FightPhase();
            newPhase.Deserialize(reader);
        }

    }
}


