using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AllianceInsiderPrismInformation : PrismInformation  
    { 
        public new const ushort Id = 3284;
        public override ushort TypeId => Id;

        public int lastTimeSlotModificationDate;
        public int lastTimeSlotModificationAuthorGuildId;
        public long lastTimeSlotModificationAuthorId;
        public string lastTimeSlotModificationAuthorName;
        public ObjectItem[] modulesObjects;

        public AllianceInsiderPrismInformation()
        {
        }
        public AllianceInsiderPrismInformation(int lastTimeSlotModificationDate,int lastTimeSlotModificationAuthorGuildId,long lastTimeSlotModificationAuthorId,string lastTimeSlotModificationAuthorName,ObjectItem[] modulesObjects,byte typeId,byte state,int nextVulnerabilityDate,int placementDate,int rewardTokenCount)
        {
            this.lastTimeSlotModificationDate = lastTimeSlotModificationDate;
            this.lastTimeSlotModificationAuthorGuildId = lastTimeSlotModificationAuthorGuildId;
            this.lastTimeSlotModificationAuthorId = lastTimeSlotModificationAuthorId;
            this.lastTimeSlotModificationAuthorName = lastTimeSlotModificationAuthorName;
            this.modulesObjects = modulesObjects;
            this.typeId = typeId;
            this.state = state;
            this.nextVulnerabilityDate = nextVulnerabilityDate;
            this.placementDate = placementDate;
            this.rewardTokenCount = rewardTokenCount;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (lastTimeSlotModificationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + lastTimeSlotModificationDate + ") on element lastTimeSlotModificationDate.");
            }

            writer.WriteInt((int)lastTimeSlotModificationDate);
            if (lastTimeSlotModificationAuthorGuildId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastTimeSlotModificationAuthorGuildId + ") on element lastTimeSlotModificationAuthorGuildId.");
            }

            writer.WriteVarInt((int)lastTimeSlotModificationAuthorGuildId);
            if (lastTimeSlotModificationAuthorId < 0 || lastTimeSlotModificationAuthorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + lastTimeSlotModificationAuthorId + ") on element lastTimeSlotModificationAuthorId.");
            }

            writer.WriteVarLong((long)lastTimeSlotModificationAuthorId);
            writer.WriteUTF((string)lastTimeSlotModificationAuthorName);
            writer.WriteShort((short)modulesObjects.Length);
            for (uint _i5 = 0;_i5 < modulesObjects.Length;_i5++)
            {
                (modulesObjects[_i5] as ObjectItem).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            ObjectItem _item5 = null;
            base.Deserialize(reader);
            lastTimeSlotModificationDate = (int)reader.ReadInt();
            if (lastTimeSlotModificationDate < 0)
            {
                throw new System.Exception("Forbidden value (" + lastTimeSlotModificationDate + ") on element of AllianceInsiderPrismInformation.lastTimeSlotModificationDate.");
            }

            lastTimeSlotModificationAuthorGuildId = (int)reader.ReadVarUhInt();
            if (lastTimeSlotModificationAuthorGuildId < 0)
            {
                throw new System.Exception("Forbidden value (" + lastTimeSlotModificationAuthorGuildId + ") on element of AllianceInsiderPrismInformation.lastTimeSlotModificationAuthorGuildId.");
            }

            lastTimeSlotModificationAuthorId = (long)reader.ReadVarUhLong();
            if (lastTimeSlotModificationAuthorId < 0 || lastTimeSlotModificationAuthorId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + lastTimeSlotModificationAuthorId + ") on element of AllianceInsiderPrismInformation.lastTimeSlotModificationAuthorId.");
            }

            lastTimeSlotModificationAuthorName = (string)reader.ReadUTF();
            uint _modulesObjectsLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _modulesObjectsLen;_i5++)
            {
                _item5 = new ObjectItem();
                _item5.Deserialize(reader);
                modulesObjects[_i5] = _item5;
            }

        }


    }
}








