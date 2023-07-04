using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceFightFighterRemovedMessage : NetworkMessage
    {
        public const ushort Id = 7194;
        public override ushort MessageId => Id;

        public SocialFightInfo allianceFightInfo;
        public long fighterId;

        public AllianceFightFighterRemovedMessage()
        {
        }
        public AllianceFightFighterRemovedMessage(SocialFightInfo allianceFightInfo, long fighterId)
        {
            this.allianceFightInfo = allianceFightInfo;
            this.fighterId = fighterId;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceFightInfo.Serialize(writer);
            if (fighterId < 0 || fighterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + fighterId + ") on element fighterId.");
            }

            writer.WriteVarLong((long)fighterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceFightInfo = new SocialFightInfo();
            allianceFightInfo.Deserialize(reader);
            fighterId = (long)reader.ReadVarUhLong();
            if (fighterId < 0 || fighterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + fighterId + ") on element of AllianceFightFighterRemovedMessage.fighterId.");
            }

        }

    }
}


