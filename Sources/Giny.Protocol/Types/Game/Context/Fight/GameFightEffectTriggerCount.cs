using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class GameFightEffectTriggerCount
    {
        public const ushort Id = 4051;
        public virtual ushort TypeId => Id;

        public int effectId;
        public double targetId;
        public short count;

        public GameFightEffectTriggerCount()
        {
        }
        public GameFightEffectTriggerCount(int effectId, double targetId, short count)
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
            if (targetId < -9007199254740992 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            if (count < 0)
            {
                throw new System.Exception("Forbidden value (" + count + ") on element count.");
            }

            writer.WriteShort((short)count);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            effectId = (int)reader.ReadVarUhInt();
            if (effectId < 0)
            {
                throw new System.Exception("Forbidden value (" + effectId + ") on element of GameFightEffectTriggerCount.effectId.");
            }

            targetId = (double)reader.ReadDouble();
            if (targetId < -9007199254740992 || targetId > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of GameFightEffectTriggerCount.targetId.");
            }

            count = (short)reader.ReadShort();
            if (count < 0)
            {
                throw new System.Exception("Forbidden value (" + count + ") on element of GameFightEffectTriggerCount.count.");
            }

        }


    }
}


