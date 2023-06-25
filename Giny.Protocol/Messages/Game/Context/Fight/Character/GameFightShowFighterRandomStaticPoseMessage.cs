using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightShowFighterRandomStaticPoseMessage : GameFightShowFighterMessage  
    { 
        public new const ushort Id = 3166;
        public override ushort MessageId => Id;


        public GameFightShowFighterRandomStaticPoseMessage()
        {
        }
        public GameFightShowFighterRandomStaticPoseMessage(GameFightFighterInformations informations)
        {
            this.informations = informations;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }


    }
}








