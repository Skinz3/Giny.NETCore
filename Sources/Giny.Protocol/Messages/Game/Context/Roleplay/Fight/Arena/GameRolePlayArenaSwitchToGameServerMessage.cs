using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameRolePlayArenaSwitchToGameServerMessage : NetworkMessage
    {
        public const ushort Id = 9037;
        public override ushort MessageId => Id;

        public bool validToken;
        public string token;
        public short homeServerId;

        public GameRolePlayArenaSwitchToGameServerMessage()
        {
        }
        public GameRolePlayArenaSwitchToGameServerMessage(bool validToken, string token, short homeServerId)
        {
            this.validToken = validToken;
            this.token = token;
            this.homeServerId = homeServerId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)validToken);
            writer.WriteUTF((string)token);
            writer.WriteShort((short)homeServerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            validToken = (bool)reader.ReadBoolean();
            token = (string)reader.ReadUTF();
            homeServerId = (short)reader.ReadShort();
        }

    }
}


