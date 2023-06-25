using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FighterStatsListMessage : NetworkMessage  
    { 
        public  const ushort Id = 2371;
        public override ushort MessageId => Id;

        public CharacterCharacteristicsInformations stats;

        public FighterStatsListMessage()
        {
        }
        public FighterStatsListMessage(CharacterCharacteristicsInformations stats)
        {
            this.stats = stats;
        }
        public override void Serialize(IDataWriter writer)
        {
            stats.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            stats = new CharacterCharacteristicsInformations();
            stats.Deserialize(reader);
        }


    }
}








