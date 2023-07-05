using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameActionItemListMessage : NetworkMessage
    {
        public const ushort Id = 1976;
        public override ushort MessageId => Id;

        public GameActionItem[] actions;

        public GameActionItemListMessage()
        {
        }
        public GameActionItemListMessage(GameActionItem[] actions)
        {
            this.actions = actions;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)actions.Length);
            for (uint _i1 = 0; _i1 < actions.Length; _i1++)
            {
                (actions[_i1] as GameActionItem).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            GameActionItem _item1 = null;
            uint _actionsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0; _i1 < _actionsLen; _i1++)
            {
                _item1 = new GameActionItem();
                _item1.Deserialize(reader);
                actions[_i1] = _item1;
            }

        }

    }
}


