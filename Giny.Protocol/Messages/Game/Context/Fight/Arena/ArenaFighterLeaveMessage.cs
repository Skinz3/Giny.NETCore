using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ArenaFighterLeaveMessage : NetworkMessage  
    { 
        public  const ushort Id = 8201;
        public override ushort MessageId => Id;

        public CharacterBasicMinimalInformations leaver;

        public ArenaFighterLeaveMessage()
        {
        }
        public ArenaFighterLeaveMessage(CharacterBasicMinimalInformations leaver)
        {
            this.leaver = leaver;
        }
        public override void Serialize(IDataWriter writer)
        {
            leaver.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            leaver = new CharacterBasicMinimalInformations();
            leaver.Deserialize(reader);
        }


    }
}








