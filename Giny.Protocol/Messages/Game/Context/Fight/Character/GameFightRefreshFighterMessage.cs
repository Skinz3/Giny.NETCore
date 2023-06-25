using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightRefreshFighterMessage : NetworkMessage  
    { 
        public  const ushort Id = 8487;
        public override ushort MessageId => Id;

        public GameContextActorInformations informations;

        public GameFightRefreshFighterMessage()
        {
        }
        public GameFightRefreshFighterMessage(GameContextActorInformations informations)
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
            informations = ProtocolTypeManager.GetInstance<GameContextActorInformations>((short)_id1);
            informations.Deserialize(reader);
        }


    }
}








