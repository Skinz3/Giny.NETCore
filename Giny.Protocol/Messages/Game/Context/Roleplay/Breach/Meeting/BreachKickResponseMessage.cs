using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class BreachKickResponseMessage : NetworkMessage  
    { 
        public  const ushort Id = 6036;
        public override ushort MessageId => Id;

        public CharacterMinimalInformations target;
        public bool kicked;

        public BreachKickResponseMessage()
        {
        }
        public BreachKickResponseMessage(CharacterMinimalInformations target,bool kicked)
        {
            this.target = target;
            this.kicked = kicked;
        }
        public override void Serialize(IDataWriter writer)
        {
            target.Serialize(writer);
            writer.WriteBoolean((bool)kicked);
        }
        public override void Deserialize(IDataReader reader)
        {
            target = new CharacterMinimalInformations();
            target.Deserialize(reader);
            kicked = (bool)reader.ReadBoolean();
        }


    }
}








