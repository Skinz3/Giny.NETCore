using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterSelectedSuccessMessage : NetworkMessage  
    { 
        public  const ushort Id = 2878;
        public override ushort MessageId => Id;

        public CharacterBaseInformations infos;
        public bool isCollectingStats;

        public CharacterSelectedSuccessMessage()
        {
        }
        public CharacterSelectedSuccessMessage(CharacterBaseInformations infos,bool isCollectingStats)
        {
            this.infos = infos;
            this.isCollectingStats = isCollectingStats;
        }
        public override void Serialize(IDataWriter writer)
        {
            infos.Serialize(writer);
            writer.WriteBoolean((bool)isCollectingStats);
        }
        public override void Deserialize(IDataReader reader)
        {
            infos = new CharacterBaseInformations();
            infos.Deserialize(reader);
            isCollectingStats = (bool)reader.ReadBoolean();
        }


    }
}








