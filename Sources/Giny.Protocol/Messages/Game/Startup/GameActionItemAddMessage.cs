using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameActionItemAddMessage : NetworkMessage
    {
        public const ushort Id = 8600;
        public override ushort MessageId => Id;

        public GameActionItem newAction;

        public GameActionItemAddMessage()
        {
        }
        public GameActionItemAddMessage(GameActionItem newAction)
        {
            this.newAction = newAction;
        }
        public override void Serialize(IDataWriter writer)
        {
            newAction.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            newAction = new GameActionItem();
            newAction.Deserialize(reader);
        }

    }
}


