using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PartyMemberArenaInformations : PartyMemberInformations  
    { 
        public new const ushort Id = 5261;
        public override ushort TypeId => Id;

        public short rank;

        public PartyMemberArenaInformations()
        {
        }
        public PartyMemberArenaInformations(short rank,long id,string name,short level,EntityLook entityLook,byte breed,bool sex,int lifePoints,int maxLifePoints,short prospecting,byte regenRate,short initiative,byte alignmentSide,short worldX,short worldY,double mapId,short subAreaId,PlayerStatus status,PartyEntityBaseInformation[] entities)
        {
            this.rank = rank;
            this.id = id;
            this.name = name;
            this.level = level;
            this.entityLook = entityLook;
            this.breed = breed;
            this.sex = sex;
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.prospecting = prospecting;
            this.regenRate = regenRate;
            this.initiative = initiative;
            this.alignmentSide = alignmentSide;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.status = status;
            this.entities = entities;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (rank < 0 || rank > 20000)
            {
                throw new System.Exception("Forbidden value (" + rank + ") on element rank.");
            }

            writer.WriteVarShort((short)rank);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            rank = (short)reader.ReadVarUhShort();
            if (rank < 0 || rank > 20000)
            {
                throw new System.Exception("Forbidden value (" + rank + ") on element of PartyMemberArenaInformations.rank.");
            }

        }


    }
}








