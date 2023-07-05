using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ReloginTokenStatusMessage : NetworkMessage
    {
        public const ushort Id = 8771;
        public override ushort MessageId => Id;

        public bool validToken;
        public string token;

        public ReloginTokenStatusMessage()
        {
        }
        public ReloginTokenStatusMessage(bool validToken, string token)
        {
            this.validToken = validToken;
            this.token = token;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)validToken);
            writer.WriteUTF((string)token);
        }
        public override void Deserialize(IDataReader reader)
        {
            validToken = (bool)reader.ReadBoolean();
            token = (string)reader.ReadUTF();
        }

    }
}


