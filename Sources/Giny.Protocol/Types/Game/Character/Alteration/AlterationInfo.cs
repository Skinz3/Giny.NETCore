using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class AlterationInfo
    {
        public const ushort Id = 980;
        public virtual ushort TypeId => Id;

        public uint alterationId;
        public double creationTime;
        public byte expirationType;
        public double expirationValue;
        public ObjectEffect[] effects;

        public AlterationInfo()
        {
        }
        public AlterationInfo(uint alterationId, double creationTime, byte expirationType, double expirationValue, ObjectEffect[] effects)
        {
            this.alterationId = alterationId;
            this.creationTime = creationTime;
            this.expirationType = expirationType;
            this.expirationValue = expirationValue;
            this.effects = effects;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (alterationId < 0 || alterationId > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + alterationId + ") on element alterationId.");
            }

            writer.WriteUInt((uint)alterationId);
            if (creationTime < -9007199254740992 || creationTime > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + creationTime + ") on element creationTime.");
            }

            writer.WriteDouble((double)creationTime);
            writer.WriteByte((byte)expirationType);
            if (expirationValue < -9007199254740992 || expirationValue > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + expirationValue + ") on element expirationValue.");
            }

            writer.WriteDouble((double)expirationValue);
            writer.WriteShort((short)effects.Length);
            for (uint _i5 = 0; _i5 < effects.Length; _i5++)
            {
                writer.WriteShort((short)(effects[_i5] as ObjectEffect).TypeId);
                (effects[_i5] as ObjectEffect).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _id5 = 0;
            ObjectEffect _item5 = null;
            alterationId = (uint)reader.ReadUInt();
            if (alterationId < 0 || alterationId > 4294967295)
            {
                throw new System.Exception("Forbidden value (" + alterationId + ") on element of AlterationInfo.alterationId.");
            }

            creationTime = (double)reader.ReadDouble();
            if (creationTime < -9007199254740992 || creationTime > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + creationTime + ") on element of AlterationInfo.creationTime.");
            }

            expirationType = (byte)reader.ReadByte();
            if (expirationType < 0)
            {
                throw new System.Exception("Forbidden value (" + expirationType + ") on element of AlterationInfo.expirationType.");
            }

            expirationValue = (double)reader.ReadDouble();
            if (expirationValue < -9007199254740992 || expirationValue > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + expirationValue + ") on element of AlterationInfo.expirationValue.");
            }

            uint _effectsLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0; _i5 < _effectsLen; _i5++)
            {
                _id5 = (uint)reader.ReadUShort();
                _item5 = ProtocolTypeManager.GetInstance<ObjectEffect>((short)_id5);
                _item5.Deserialize(reader);
                effects[_i5] = _item5;
            }

        }


    }
}


