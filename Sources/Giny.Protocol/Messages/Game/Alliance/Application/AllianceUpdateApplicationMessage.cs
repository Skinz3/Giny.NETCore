using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class AllianceUpdateApplicationMessage : NetworkMessage
    {
        public const ushort Id = 6880;
        public override ushort MessageId => Id;

        public string applyText;
        public int allianceId;

        public AllianceUpdateApplicationMessage()
        {
        }
        public AllianceUpdateApplicationMessage(string applyText, int allianceId)
        {
            this.applyText = applyText;
            this.allianceId = allianceId;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)applyText);
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element allianceId.");
            }

            writer.WriteVarInt((int)allianceId);
        }
        public override void Deserialize(IDataReader reader)
        {
            applyText = (string)reader.ReadUTF();
            allianceId = (int)reader.ReadVarUhInt();
            if (allianceId < 0)
            {
                throw new System.Exception("Forbidden value (" + allianceId + ") on element of AllianceUpdateApplicationMessage.allianceId.");
            }

        }

    }
}


