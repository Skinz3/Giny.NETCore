using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceFightFighterAddedMessage : NetworkMessage
    {
        public const ushort Id = 5166;
        public override ushort MessageId => Id;

        public SocialFightInfo allianceFightInfo;
        public CharacterMinimalPlusLookInformations fighter;
        public byte team;

        public AllianceFightFighterAddedMessage()
        {
        }
        public AllianceFightFighterAddedMessage(SocialFightInfo allianceFightInfo, CharacterMinimalPlusLookInformations fighter, byte team)
        {
            this.allianceFightInfo = allianceFightInfo;
            this.fighter = fighter;
            this.team = team;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceFightInfo.Serialize(writer);
            fighter.Serialize(writer);
            writer.WriteByte((byte)team);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceFightInfo = new SocialFightInfo();
            allianceFightInfo.Deserialize(reader);
            fighter = new CharacterMinimalPlusLookInformations();
            fighter.Deserialize(reader);
            team = (byte)reader.ReadByte();
            if (team < 0)
            {
                throw new System.Exception("Forbidden value (" + team + ") on element of AllianceFightFighterAddedMessage.team.");
            }

        }

    }
}


