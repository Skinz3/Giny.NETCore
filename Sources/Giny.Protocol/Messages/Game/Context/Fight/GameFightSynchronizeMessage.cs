using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightSynchronizeMessage : NetworkMessage  
    { 
        public  const ushort Id = 7844;
        public override ushort MessageId => Id;

        public GameFightFighterInformations[] fighters;

        public GameFightSynchronizeMessage()
        {
        }
        public GameFightSynchronizeMessage(GameFightFighterInformations[] fighters)
        {
            this.fighters = fighters;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)fighters.Length);
            for (uint _i1 = 0;_i1 < fighters.Length;_i1++)
            {
                writer.WriteShort((short)(fighters[_i1] as GameFightFighterInformations).TypeId);
                (fighters[_i1] as GameFightFighterInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            GameFightFighterInformations _item1 = null;
            uint _fightersLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _fightersLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<GameFightFighterInformations>((short)_id1);
                _item1.Deserialize(reader);
                fighters[_i1] = _item1;
            }

        }


    }
}








