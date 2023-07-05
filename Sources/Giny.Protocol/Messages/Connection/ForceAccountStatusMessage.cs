using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ForceAccountStatusMessage : NetworkMessage
    {
        public const ushort Id = 7528;
        public override ushort MessageId => Id;

        public bool force;
        public int forcedAccountId;
        public string forcedNickname;
        public string forcedTag;

        public ForceAccountStatusMessage()
        {
        }
        public ForceAccountStatusMessage(bool force, int forcedAccountId, string forcedNickname, string forcedTag)
        {
            this.force = force;
            this.forcedAccountId = forcedAccountId;
            this.forcedNickname = forcedNickname;
            this.forcedTag = forcedTag;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)force);
            if (forcedAccountId < 0)
            {
                throw new System.Exception("Forbidden value (" + forcedAccountId + ") on element forcedAccountId.");
            }

            writer.WriteInt((int)forcedAccountId);
            writer.WriteUTF((string)forcedNickname);
            writer.WriteUTF((string)forcedTag);
        }
        public override void Deserialize(IDataReader reader)
        {
            force = (bool)reader.ReadBoolean();
            forcedAccountId = (int)reader.ReadInt();
            if (forcedAccountId < 0)
            {
                throw new System.Exception("Forbidden value (" + forcedAccountId + ") on element of ForceAccountStatusMessage.forcedAccountId.");
            }

            forcedNickname = (string)reader.ReadUTF();
            forcedTag = (string)reader.ReadUTF();
        }

    }
}


