using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class CharacterCreationResultMessage : NetworkMessage
    {
        public const ushort Id = 5976;
        public override ushort MessageId => Id;

        public byte result;
        public byte reason;

        public CharacterCreationResultMessage()
        {
        }
        public CharacterCreationResultMessage(byte result, byte reason)
        {
            this.result = result;
            this.reason = reason;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)result);
            writer.WriteByte((byte)reason);
        }
        public override void Deserialize(IDataReader reader)
        {
            result = (byte)reader.ReadByte();
            if (result < 0)
            {
                throw new System.Exception("Forbidden value (" + result + ") on element of CharacterCreationResultMessage.result.");
            }

            reason = (byte)reader.ReadByte();
            if (reason < 0)
            {
                throw new System.Exception("Forbidden value (" + reason + ") on element of CharacterCreationResultMessage.reason.");
            }

        }

    }
}


