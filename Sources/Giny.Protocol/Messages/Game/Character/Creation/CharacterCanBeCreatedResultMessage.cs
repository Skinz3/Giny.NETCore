using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterCanBeCreatedResultMessage : NetworkMessage  
    { 
        public  const ushort Id = 6756;
        public override ushort MessageId => Id;

        public bool yesYouCan;

        public CharacterCanBeCreatedResultMessage()
        {
        }
        public CharacterCanBeCreatedResultMessage(bool yesYouCan)
        {
            this.yesYouCan = yesYouCan;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)yesYouCan);
        }
        public override void Deserialize(IDataReader reader)
        {
            yesYouCan = (bool)reader.ReadBoolean();
        }


    }
}








