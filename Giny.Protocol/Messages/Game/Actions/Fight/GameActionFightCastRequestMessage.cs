using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameActionFightCastRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1857;
        public override ushort MessageId => Id;

        public short spellId;
        public short cellId;

        public GameActionFightCastRequestMessage()
        {
        }
        public GameActionFightCastRequestMessage(short spellId,short cellId)
        {
            this.spellId = spellId;
            this.cellId = cellId;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
            if (cellId < -1 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteShort((short)cellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of GameActionFightCastRequestMessage.spellId.");
            }

            cellId = (short)reader.ReadShort();
            if (cellId < -1 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of GameActionFightCastRequestMessage.cellId.");
            }

        }


    }
}








