using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameRolePlayPlayerFightRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 4692;
        public override ushort MessageId => Id;

        public long targetId;
        public short targetCellId;
        public bool friendly;

        public GameRolePlayPlayerFightRequestMessage()
        {
        }
        public GameRolePlayPlayerFightRequestMessage(long targetId,short targetCellId,bool friendly)
        {
            this.targetId = targetId;
            this.targetCellId = targetCellId;
            this.friendly = friendly;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (targetId < 0 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteVarLong((long)targetId);
            if (targetCellId < -1 || targetCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + targetCellId + ") on element targetCellId.");
            }

            writer.WriteShort((short)targetCellId);
            writer.WriteBoolean((bool)friendly);
        }
        public override void Deserialize(IDataReader reader)
        {
            targetId = (long)reader.ReadVarUhLong();
            if (targetId < 0 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameRolePlayPlayerFightRequestMessage.targetId.");
            }

            targetCellId = (short)reader.ReadShort();
            if (targetCellId < -1 || targetCellId > 559)
            {
                throw new System.Exception("Forbidden value (" + targetCellId + ") on element of GameRolePlayPlayerFightRequestMessage.targetCellId.");
            }

            friendly = (bool)reader.ReadBoolean();
        }


    }
}








