using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class SpellVariantActivationMessage : NetworkMessage  
    { 
        public  const ushort Id = 6118;
        public override ushort MessageId => Id;

        public short spellId;
        public bool result;

        public SpellVariantActivationMessage()
        {
        }
        public SpellVariantActivationMessage(short spellId,bool result)
        {
            this.spellId = spellId;
            this.result = result;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
            writer.WriteBoolean((bool)result);
        }
        public override void Deserialize(IDataReader reader)
        {
            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of SpellVariantActivationMessage.spellId.");
            }

            result = (bool)reader.ReadBoolean();
        }


    }
}








