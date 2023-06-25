using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class PartyMemberInformations : CharacterBaseInformations  
    { 
        public new const ushort Id = 8826;
        public override ushort TypeId => Id;

        public int lifePoints;
        public int maxLifePoints;
        public short prospecting;
        public byte regenRate;
        public short initiative;
        public byte alignmentSide;
        public short worldX;
        public short worldY;
        public double mapId;
        public short subAreaId;
        public PlayerStatus status;
        public PartyEntityBaseInformation[] entities;

        public PartyMemberInformations()
        {
        }
        public PartyMemberInformations(int lifePoints,int maxLifePoints,short prospecting,byte regenRate,short initiative,byte alignmentSide,short worldX,short worldY,double mapId,short subAreaId,PlayerStatus status,PartyEntityBaseInformation[] entities,long id,string name,short level,EntityLook entityLook,byte breed,bool sex)
        {
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
            this.id = id;
            this.name = name;
            this.level = level;
            this.entityLook = entityLook;
            this.breed = breed;
            this.sex = sex;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
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
            if (initiative < 0)
            {
                throw new System.Exception("Forbidden value (" + initiative + ") on element initiative.");
            }

            writer.WriteVarShort((short)initiative);
            writer.WriteByte((byte)alignmentSide);
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element worldX.");
            }

            writer.WriteShort((short)worldX);
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element worldY.");
            }

            writer.WriteShort((short)worldY);
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element mapId.");
            }

            writer.WriteDouble((double)mapId);
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element subAreaId.");
            }

            writer.WriteVarShort((short)subAreaId);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
            writer.WriteShort((short)entities.Length);
            for (uint _i12 = 0;_i12 < entities.Length;_i12++)
            {
                writer.WriteShort((short)(entities[_i12] as PartyEntityBaseInformation).TypeId);
                (entities[_i12] as PartyEntityBaseInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id12 = 0;
            PartyEntityBaseInformation _item12 = null;
            base.Deserialize(reader);
            lifePoints = (int)reader.ReadVarUhInt();
            if (lifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + lifePoints + ") on element of PartyMemberInformations.lifePoints.");
            }

            maxLifePoints = (int)reader.ReadVarUhInt();
            if (maxLifePoints < 0)
            {
                throw new System.Exception("Forbidden value (" + maxLifePoints + ") on element of PartyMemberInformations.maxLifePoints.");
            }

            prospecting = (short)reader.ReadVarUhShort();
            if (prospecting < 0)
            {
                throw new System.Exception("Forbidden value (" + prospecting + ") on element of PartyMemberInformations.prospecting.");
            }

            regenRate = (byte)reader.ReadSByte();
            if (regenRate < 0 || regenRate > 255)
            {
                throw new System.Exception("Forbidden value (" + regenRate + ") on element of PartyMemberInformations.regenRate.");
            }

            initiative = (short)reader.ReadVarUhShort();
            if (initiative < 0)
            {
                throw new System.Exception("Forbidden value (" + initiative + ") on element of PartyMemberInformations.initiative.");
            }

            alignmentSide = (byte)reader.ReadByte();
            worldX = (short)reader.ReadShort();
            if (worldX < -255 || worldX > 255)
            {
                throw new System.Exception("Forbidden value (" + worldX + ") on element of PartyMemberInformations.worldX.");
            }

            worldY = (short)reader.ReadShort();
            if (worldY < -255 || worldY > 255)
            {
                throw new System.Exception("Forbidden value (" + worldY + ") on element of PartyMemberInformations.worldY.");
            }

            mapId = (double)reader.ReadDouble();
            if (mapId < 0 || mapId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + mapId + ") on element of PartyMemberInformations.mapId.");
            }

            subAreaId = (short)reader.ReadVarUhShort();
            if (subAreaId < 0)
            {
                throw new System.Exception("Forbidden value (" + subAreaId + ") on element of PartyMemberInformations.subAreaId.");
            }

            uint _id11 = (uint)reader.ReadUShort();
            status = ProtocolTypeManager.GetInstance<PlayerStatus>((short)_id11);
            status.Deserialize(reader);
            uint _entitiesLen = (uint)reader.ReadUShort();
            for (uint _i12 = 0;_i12 < _entitiesLen;_i12++)
            {
                _id12 = (uint)reader.ReadUShort();
                _item12 = ProtocolTypeManager.GetInstance<PartyEntityBaseInformation>((short)_id12);
                _item12.Deserialize(reader);
                entities[_i12] = _item12;
            }

        }


    }
}








