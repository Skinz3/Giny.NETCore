using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ForgettableSpellClientActionMessage : NetworkMessage  
    { 
        public  const ushort Id = 1079;
        public override ushort MessageId => Id;

        public int spellId;
        public byte action;

        public ForgettableSpellClientActionMessage()
        {
        }
        public ForgettableSpellClientActionMessage(int spellId,byte action)
        {
            this.spellId = spellId;
            this.action = action;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteInt((int)spellId);
            writer.WriteByte((byte)action);
        }
        public override void Deserialize(IDataReader reader)
        {
            spellId = (int)reader.ReadInt();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of ForgettableSpellClientActionMessage.spellId.");
            }

            action = (byte)reader.ReadByte();
            if (action < 0)
            {
                throw new System.Exception("Forbidden value (" + action + ") on element of ForgettableSpellClientActionMessage.action.");
            }

        }


    }
}








