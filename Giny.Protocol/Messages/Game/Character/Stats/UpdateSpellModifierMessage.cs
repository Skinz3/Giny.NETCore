using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class UpdateSpellModifierMessage : NetworkMessage  
    { 
        public  const ushort Id = 9587;
        public override ushort MessageId => Id;

        public double actorId;
        public CharacterSpellModification spellModifier;

        public UpdateSpellModifierMessage()
        {
        }
        public UpdateSpellModifierMessage(double actorId,CharacterSpellModification spellModifier)
        {
            this.actorId = actorId;
            this.spellModifier = spellModifier;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (actorId < -9.00719925474099E+15 || actorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element actorId.");
            }

            writer.WriteDouble((double)actorId);
            spellModifier.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            actorId = (double)reader.ReadDouble();
            if (actorId < -9.00719925474099E+15 || actorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + actorId + ") on element of UpdateSpellModifierMessage.actorId.");
            }

            spellModifier = new CharacterSpellModification();
            spellModifier.Deserialize(reader);
        }


    }
}








