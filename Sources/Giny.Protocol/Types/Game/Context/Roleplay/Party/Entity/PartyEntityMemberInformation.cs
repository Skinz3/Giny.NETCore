using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PartyEntityMemberInformation : PartyEntityBaseInformation  
    { 
        public new const ushort Id = 8077;
        public override ushort TypeId => Id;

        public short initiative;
        public int lifePoints;
        public int maxLifePoints;
        public short prospecting;
        public byte regenRate;

        public PartyEntityMemberInformation()
        {
        }
        public PartyEntityMemberInformation(short initiative,int lifePoints,int maxLifePoints,short prospecting,byte regenRate,byte indexId,byte entityModelId,EntityLook entityLook)
        {
            this.initiative = initiative;
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.prospecting = prospecting;
            this.regenRate = regenRate;
            this.indexId = indexId;
            this.entityModelId = entityModelId;
            this.entityLook = entityLook;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (initiative < 0)
            {
                throw new System.Exception("Forbidden value (" + initiative + ") on element initiative.");
            }

            writer.WriteVarShort((short)initiative);
            if (lifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + lifePoints + ") on element lifePoints.");
            }

            writer.WriteVarInt((int)lifePoints);
            if (maxLifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + maxLifePoints + ") on element maxLifePoints.");
            }

            writer.WriteVarInt((int)maxLifePoints);
            if (prospecting < 0)
            {
                throw new System.Exception("Forbidden value (" + prospecting + ") on element prospecting.");
            }

            writer.WriteVarShort((short)prospecting);
            if (regenRate < 0 || regenRate > 255)
            {
                throw new System.Exception("Forbidden value (" + regenRate + ") on element regenRate.");
            }

            writer.WriteByte((byte)regenRate);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            initiative = (short)reader.ReadVarUhShort();
            if (initiative < 0)
            {
                throw new System.Exception("Forbidden value (" + initiative + ") on element of PartyEntityMemberInformation.initiative.");
            }

            lifePoints = (int)reader.ReadVarUhInt();
            if (lifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + lifePoints + ") on element of PartyEntityMemberInformation.lifePoints.");
            }

            maxLifePoints = (int)reader.ReadVarUhInt();
            if (maxLifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + maxLifePoints + ") on element of PartyEntityMemberInformation.maxLifePoints.");
            }

            prospecting = (short)reader.ReadVarUhShort();
            if (prospecting < 0)
            {
                throw new System.Exception("Forbidden value (" + prospecting + ") on element of PartyEntityMemberInformation.prospecting.");
            }

            regenRate = (byte)reader.ReadSByte();
            if (regenRate < 0 || regenRate > 255)
            {
                throw new System.Exception("Forbidden value (" + regenRate + ") on element of PartyEntityMemberInformation.regenRate.");
            }

        }


    }
}








