using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StartupActionsListMessage : NetworkMessage  
    { 
        public  const ushort Id = 2329;
        public override ushort MessageId => Id;

        public StartupActionAddObject[] actions;

        public StartupActionsListMessage()
        {
        }
        public StartupActionsListMessage(StartupActionAddObject[] actions)
        {
            this.actions = actions;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)actions.Length);
            for (uint _i1 = 0;_i1 < actions.Length;_i1++)
            {
                (actions[_i1] as StartupActionAddObject).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            StartupActionAddObject _item1 = null;
            uint _actionsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _actionsLen;_i1++)
            {
                _item1 = new StartupActionAddObject();
                _item1.Deserialize(reader);
                actions[_i1] = _item1;
            }

        }


    }
}








