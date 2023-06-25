using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayShowMultipleActorsMessage : NetworkMessage  
    { 
        public  const ushort Id = 5370;
        public override ushort MessageId => Id;

        public GameRolePlayActorInformations[] informationsList;

        public GameRolePlayShowMultipleActorsMessage()
        {
        }
        public GameRolePlayShowMultipleActorsMessage(GameRolePlayActorInformations[] informationsList)
        {
            this.informationsList = informationsList;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)informationsList.Length);
            for (uint _i1 = 0;_i1 < informationsList.Length;_i1++)
            {
                writer.WriteShort((short)(informationsList[_i1] as GameRolePlayActorInformations).TypeId);
                (informationsList[_i1] as GameRolePlayActorInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            GameRolePlayActorInformations _item1 = null;
            uint _informationsListLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _informationsListLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>((short)_id1);
                _item1.Deserialize(reader);
                informationsList[_i1] = _item1;
            }

        }


    }
}








