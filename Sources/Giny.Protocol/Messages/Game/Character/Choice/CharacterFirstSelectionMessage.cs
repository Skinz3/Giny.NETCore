using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterFirstSelectionMessage : CharacterSelectionMessage  
    { 
        public new const ushort Id = 3427;
        public override ushort MessageId => Id;

        public bool doTutorial;

        public CharacterFirstSelectionMessage()
        {
        }
        public CharacterFirstSelectionMessage(bool doTutorial,long id)
        {
            this.doTutorial = doTutorial;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean((bool)doTutorial);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            doTutorial = (bool)reader.ReadBoolean();
        }


    }
}








