using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayShowActorMessage : NetworkMessage  
    { 
        public  const ushort Id = 6549;
        public override ushort MessageId => Id;

        public GameRolePlayActorInformations informations;

        public GameRolePlayShowActorMessage()
        {
        }
        public GameRolePlayShowActorMessage(GameRolePlayActorInformations informations)
        {
            this.informations = informations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)informations.TypeId);
            informations.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = (uint)reader.ReadUShort();
            informations = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>((short)_id1);
            informations.Deserialize(reader);
        }


    }
}








