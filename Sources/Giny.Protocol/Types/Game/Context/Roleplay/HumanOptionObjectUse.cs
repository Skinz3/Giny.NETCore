using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class HumanOptionObjectUse : HumanOption  
    { 
        public new const ushort Id = 9403;
        public override ushort TypeId => Id;

        public byte delayTypeId;
        public double delayEndTime;
        public short objectGID;

        public HumanOptionObjectUse()
        {
        }
        public HumanOptionObjectUse(byte delayTypeId,double delayEndTime,short objectGID)
        {
            this.delayTypeId = delayTypeId;
            this.delayEndTime = delayEndTime;
            this.objectGID = objectGID;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)delayTypeId);
            if (delayEndTime < 0 || delayEndTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + delayEndTime + ") on element delayEndTime.");
            }

            writer.WriteDouble((double)delayEndTime);
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarShort((short)objectGID);
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
            if (delayEndTime < 0 || delayEndTime > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + delayEndTime + ") on element of HumanOptionObjectUse.delayEndTime.");
            }

            objectGID = (short)reader.ReadVarUhShort();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of HumanOptionObjectUse.objectGID.");
            }

        }


    }
}








