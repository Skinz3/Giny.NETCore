using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharactersListMessage : BasicCharactersListMessage  
    { 
        public new const ushort Id = 7382;
        public override ushort MessageId => Id;

        public bool hasStartupActions;

        public CharactersListMessage()
        {
        }
        public CharactersListMessage(bool hasStartupActions,CharacterBaseInformations[] characters)
        {
            this.hasStartupActions = hasStartupActions;
            this.characters = characters;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)hasStartupActions);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            hasStartupActions = (bool)reader.ReadBoolean();
        }


    }
}








