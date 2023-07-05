using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class UpdateMapPlayersAgressableStatusMessage : NetworkMessage
    {
        public const ushort Id = 9727;
        public override ushort MessageId => Id;

        public AgressableStatusMessage[] playerAvAMessages;

        public UpdateMapPlayersAgressableStatusMessage()
        {
        }
        public UpdateMapPlayersAgressableStatusMessage(AgressableStatusMessage[] playerAvAMessages)
        {
            this.playerAvAMessages = playerAvAMessages;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)playerAvAMessages.Length);
            for (uint _i1 = 0; _i1 < playerAvAMessages.Length; _i1++)
            {
                (playerAvAMessages[_i1] as AgressableStatusMessage).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            AgressableStatusMessage _item1 = null;
            uint _playerAvAMessagesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _playerAvAMessagesLen; _i1++)
            {
                _item1 = new AgressableStatusMessage();
                _item1.Deserialize(reader);
                playerAvAMessages[_i1] = _item1;
            }

        }

    }
}


