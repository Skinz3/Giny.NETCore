using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FriendSpouseInformations  
    { 
        public const ushort Id = 9223;
        public virtual ushort TypeId => Id;

        public int spouseAccountId;
        public long spouseId;
        public string spouseName;
        public short spouseLevel;
        public byte breed;
        public byte sex;
        public EntityLook spouseEntityLook;
        public GuildInformations guildInfo;
        public byte alignmentSide;

        public FriendSpouseInformations()
        {
        }
        public FriendSpouseInformations(int spouseAccountId,long spouseId,string spouseName,short spouseLevel,byte breed,byte sex,EntityLook spouseEntityLook,GuildInformations guildInfo,byte alignmentSide)
        {
            this.spouseAccountId = spouseAccountId;
            this.spouseId = spouseId;
            this.spouseName = spouseName;
            this.spouseLevel = spouseLevel;
            this.breed = breed;
            this.sex = sex;
            this.spouseEntityLook = spouseEntityLook;
            this.guildInfo = guildInfo;
            this.alignmentSide = alignmentSide;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (spouseAccountId < 0)
            {
                throw new System.Exception("Forbidden value (" + spouseAccountId + ") on element spouseAccountId.");
            }

            writer.WriteInt((int)spouseAccountId);
            if (spouseId < 0 || spouseId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + spouseId + ") on element spouseId.");
            }

            writer.WriteVarLong((long)spouseId);
            writer.WriteUTF((string)spouseName);
            if (spouseLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + spouseLevel + ") on element spouseLevel.");
            }

            writer.WriteVarShort((short)spouseLevel);
            writer.WriteByte((byte)breed);
            writer.WriteByte((byte)sex);
            spouseEntityLook.Serialize(writer);
            guildInfo.Serialize(writer);
            writer.WriteByte((byte)alignmentSide);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            spouseAccountId = (int)reader.ReadInt();
            if (spouseAccountId < 0)
            {
                throw new System.Exception("Forbidden value (" + spouseAccountId + ") on element of FriendSpouseInformations.spouseAccountId.");
            }

            spouseId = (long)reader.ReadVarUhLong();
            if (spouseId < 0 || spouseId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + spouseId + ") on element of FriendSpouseInformations.spouseId.");
            }

            spouseName = (string)reader.ReadUTF();
            spouseLevel = (short)reader.ReadVarUhShort();
            if (spouseLevel < 0)
            {
                throw new System.Exception("Forbidden value (" + spouseLevel + ") on element of FriendSpouseInformations.spouseLevel.");
            }

            breed = (byte)reader.ReadByte();
            sex = (byte)reader.ReadByte();
            spouseEntityLook = new EntityLook();
            spouseEntityLook.Deserialize(reader);
            guildInfo = new GuildInformations();
            guildInfo.Deserialize(reader);
            alignmentSide = (byte)reader.ReadByte();
        }


    }
}








