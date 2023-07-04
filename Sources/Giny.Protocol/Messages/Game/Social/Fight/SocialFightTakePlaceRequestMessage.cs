using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class SocialFightTakePlaceRequestMessage : NetworkMessage
    {
        public const ushort Id = 7961;
        public override ushort MessageId => Id;

        public SocialFightInfo socialFightInfo;
        public long replacedCharacterId;

        public SocialFightTakePlaceRequestMessage()
        {
        }
        public SocialFightTakePlaceRequestMessage(SocialFightInfo socialFightInfo, long replacedCharacterId)
        {
            this.socialFightInfo = socialFightInfo;
            this.replacedCharacterId = replacedCharacterId;
        }
        public override void Serialize(IDataWriter writer)
        {
            socialFightInfo.Serialize(writer);
            if (replacedCharacterId < 0 || replacedCharacterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + replacedCharacterId + ") on element replacedCharacterId.");
            }

            writer.WriteVarLong((long)replacedCharacterId);
        }
        public override void Deserialize(IDataReader reader)
        {
            socialFightInfo = new SocialFightInfo();
            socialFightInfo.Deserialize(reader);
            replacedCharacterId = (long)reader.ReadVarUhLong();
            if (replacedCharacterId < 0 || replacedCharacterId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + replacedCharacterId + ") on element of SocialFightTakePlaceRequestMessage.replacedCharacterId.");
            }

        }

    }
}


