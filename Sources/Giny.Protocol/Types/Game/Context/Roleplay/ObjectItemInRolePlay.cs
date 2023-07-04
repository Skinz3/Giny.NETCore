using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class ObjectItemInRolePlay
    {
        public const ushort Id = 6080;
        public virtual ushort TypeId => Id;

        public short cellId;
        public int objectGID;

        public ObjectItemInRolePlay()
        {
        }
        public ObjectItemInRolePlay(short cellId, int objectGID)
        {
            this.cellId = cellId;
            this.objectGID = objectGID;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element cellId.");
            }

            writer.WriteVarShort((short)cellId);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarInt((int)objectGID);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            cellId = (short)reader.ReadVarUhShort();
            if (cellId < 0 || cellId > 559)
            {
                throw new System.Exception("Forbidden value (" + cellId + ") on element of ObjectItemInRolePlay.cellId.");
            }

            objectGID = (int)reader.ReadVarUhInt();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ObjectItemInRolePlay.objectGID.");
            }

        }


    }
}


