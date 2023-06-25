using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightShowFighterMessage : NetworkMessage  
    { 
        public  const ushort Id = 3785;
        public override ushort MessageId => Id;

        public GameFightFighterInformations informations;

        public GameFightShowFighterMessage()
        {
        }
        public GameFightShowFighterMessage(GameFightFighterInformations informations)
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
            informations = ProtocolTypeManager.GetInstance<GameFightFighterInformations>((short)_id1);
            informations.Deserialize(reader);
        }


    }
}








