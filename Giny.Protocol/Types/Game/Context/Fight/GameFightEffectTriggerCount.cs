using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GameFightEffectTriggerCount  
    { 
        public const ushort Id = 463;
        public virtual ushort TypeId => Id;

        public int effectId;
        public double targetId;
        public byte count;

        public GameFightEffectTriggerCount()
        {
        }
        public GameFightEffectTriggerCount(int effectId,double targetId,byte count)
        {
            this.effectId = effectId;
            this.targetId = targetId;
            this.count = count;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (effectId < 0)
            {
                throw new System.Exception("Forbidden value (" + effectId + ") on element effectId.");
            }

            writer.WriteVarInt((int)effectId);
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            if (count < 0)
            {
                throw new System.Exception("Forbidden value (" + count + ") on element count.");
            }

            writer.WriteByte((byte)count);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            effectId = (int)reader.ReadVarUhInt();
            if (effectId < 0)
            {
                throw new System.Exception("Forbidden value (" + effectId + ") on element of GameFightEffectTriggerCount.effectId.");
            }

            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameFightEffectTriggerCount.targetId.");
            }

            count = (byte)reader.ReadByte();
            if (count < 0)
            {
                throw new System.Exception("Forbidden value (" + count + ") on element of GameFightEffectTriggerCount.count.");
            }

        }


    }
}








