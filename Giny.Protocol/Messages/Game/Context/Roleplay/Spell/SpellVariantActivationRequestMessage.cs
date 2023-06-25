using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SpellVariantActivationRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 5364;
        public override ushort MessageId => Id;

        public short spellId;

        public SpellVariantActivationRequestMessage()
        {
        }
        public SpellVariantActivationRequestMessage(short spellId)
        {
            this.spellId = spellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of SpellVariantActivationRequestMessage.spellId.");
            }

        }


    }
}








