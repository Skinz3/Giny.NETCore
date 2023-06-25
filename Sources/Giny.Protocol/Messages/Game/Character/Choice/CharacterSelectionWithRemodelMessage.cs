using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class CharacterSelectionWithRemodelMessage : CharacterSelectionMessage  
    { 
        public new const ushort Id = 772;
        public override ushort MessageId => Id;

        public RemodelingInformation remodel;

        public CharacterSelectionWithRemodelMessage()
        {
        }
        public CharacterSelectionWithRemodelMessage(RemodelingInformation remodel,long id)
        {
            this.remodel = remodel;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            remodel.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            remodel = new RemodelingInformation();
            remodel.Deserialize(reader);
        }


    }
}








