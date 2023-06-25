using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class InteractiveUsedMessage : NetworkMessage  
    { 
        public  const ushort Id = 1847;
        public override ushort MessageId => Id;

        public long entityId;
        public int elemId;
        public short skillId;
        public short duration;
        public bool canMove;

        public InteractiveUsedMessage()
        {
        }
        public InteractiveUsedMessage(long entityId,int elemId,short skillId,short duration,bool canMove)
        {
            this.entityId = entityId;
            this.elemId = elemId;
            this.skillId = skillId;
            this.duration = duration;
            this.canMove = canMove;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (entityId < 0 || entityId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + entityId + ") on element entityId.");
            }

            writer.WriteVarLong((long)entityId);
            if (elemId < 0)
            {
                throw new System.Exception("Forbidden value (" + elemId + ") on element elemId.");
            }

            writer.WriteVarInt((int)elemId);
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element skillId.");
            }

            writer.WriteVarShort((short)skillId);
            if (duration < 0)
            {
                throw new System.Exception("Forbidden value (" + duration + ") on element duration.");
            }

            writer.WriteVarShort((short)duration);
            writer.WriteBoolean((bool)canMove);
        }
        public override void Deserialize(IDataReader reader)
        {
            entityId = (long)reader.ReadVarUhLong();
            if (entityId < 0 || entityId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + entityId + ") on element of InteractiveUsedMessage.entityId.");
            }

            elemId = (int)reader.ReadVarUhInt();
            if (elemId < 0)
            {
                throw new System.Exception("Forbidden value (" + elemId + ") on element of InteractiveUsedMessage.elemId.");
            }

            skillId = (short)reader.ReadVarUhShort();
            if (skillId < 0)
            {
                throw new System.Exception("Forbidden value (" + skillId + ") on element of InteractiveUsedMessage.skillId.");
            }

            duration = (short)reader.ReadVarUhShort();
            if (duration < 0)
            {
                throw new System.Exception("Forbidden value (" + duration + ") on element of InteractiveUsedMessage.duration.");
            }

            canMove = (bool)reader.ReadBoolean();
        }


    }
}








