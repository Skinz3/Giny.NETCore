using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class HumanOptionObjectUse : HumanOption
    {
        public new const ushort Id = 4513;
        public override ushort TypeId => Id;

        public byte delayTypeId;
        public double delayEndTime;
        public int objectGID;

        public HumanOptionObjectUse()
        {
        }
        public HumanOptionObjectUse(byte delayTypeId, double delayEndTime, int objectGID)
        {
            this.delayTypeId = delayTypeId;
            this.delayEndTime = delayEndTime;
            this.objectGID = objectGID;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)delayTypeId);
            if (delayEndTime < 0 || delayEndTime > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + delayEndTime + ") on element delayEndTime.");
            }

            writer.WriteDouble((double)delayEndTime);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarInt((int)objectGID);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            delayTypeId = (byte)reader.ReadByte();
            if (delayTypeId < 0)
            {
                throw new System.Exception("Forbidden value (" + delayTypeId + ") on element of HumanOptionObjectUse.delayTypeId.");
            }

            delayEndTime = (double)reader.ReadDouble();
            if (delayEndTime < 0 || delayEndTime > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + delayEndTime + ") on element of HumanOptionObjectUse.delayEndTime.");
            }

            objectGID = (int)reader.ReadVarUhInt();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of HumanOptionObjectUse.objectGID.");
            }

        }


    }
}


