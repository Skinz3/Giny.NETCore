using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceJoinedMessage : NetworkMessage  
    { 
        public  const ushort Id = 8742;
        public override ushort MessageId => Id;

        public AllianceInformations allianceInfo;
        public bool enabled;
        public int leadingGuildId;

        public AllianceJoinedMessage()
        {
        }
        public AllianceJoinedMessage(AllianceInformations allianceInfo,bool enabled,int leadingGuildId)
        {
            this.allianceInfo = allianceInfo;
            this.enabled = enabled;
            this.leadingGuildId = leadingGuildId;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceInfo.Serialize(writer);
            writer.WriteBoolean((bool)enabled);
            if (leadingGuildId < 0)
            {
                throw new System.Exception("Forbidden value (" + leadingGuildId + ") on element leadingGuildId.");
            }

            writer.WriteVarInt((int)leadingGuildId);
        }
        public override void Deserialize(IDataReader reader)
        {
            allianceInfo = new AllianceInformations();
            allianceInfo.Deserialize(reader);
            enabled = (bool)reader.ReadBoolean();
            leadingGuildId = (int)reader.ReadVarUhInt();
            if (leadingGuildId < 0)
            {
                throw new System.Exception("Forbidden value (" + leadingGuildId + ") on element of AllianceJoinedMessage.leadingGuildId.");
            }

        }


    }
}








