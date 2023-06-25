using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class AbstractFightDispellableEffect  
    { 
        public const ushort Id = 1355;
        public virtual ushort TypeId => Id;

        public int uid;
        public double targetId;
        public short turnDuration;
        public byte dispelable;
        public short spellId;
        public int effectId;
        public int parentBoostUid;

        public AbstractFightDispellableEffect()
        {
        }
        public AbstractFightDispellableEffect(int uid,double targetId,short turnDuration,byte dispelable,short spellId,int effectId,int parentBoostUid)
        {
            this.uid = uid;
            this.targetId = targetId;
            this.turnDuration = turnDuration;
            this.dispelable = dispelable;
            this.spellId = spellId;
            this.effectId = effectId;
            this.parentBoostUid = parentBoostUid;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element uid.");
            }

            writer.WriteVarInt((int)uid);
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element targetId.");
            }

            writer.WriteDouble((double)targetId);
            writer.WriteShort((short)turnDuration);
            writer.WriteByte((byte)dispelable);
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element spellId.");
            }

            writer.WriteVarShort((short)spellId);
            if (effectId < 0)
            {
                throw new System.Exception("Forbidden value (" + effectId + ") on element effectId.");
            }

            writer.WriteVarInt((int)effectId);
            if (parentBoostUid < 0)
            {
                throw new System.Exception("Forbidden value (" + parentBoostUid + ") on element parentBoostUid.");
            }

            writer.WriteVarInt((int)parentBoostUid);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            uid = (int)reader.ReadVarUhInt();
            if (uid < 0)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element of AbstractFightDispellableEffect.uid.");
            }

            targetId = (double)reader.ReadDouble();
            if (targetId < -9.00719925474099E+15 || targetId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + targetId + ") on element of AbstractFightDispellableEffect.targetId.");
            }

            turnDuration = (short)reader.ReadShort();
            dispelable = (byte)reader.ReadByte();
            if (dispelable < 0)
            {
                throw new System.Exception("Forbidden value (" + dispelable + ") on element of AbstractFightDispellableEffect.dispelable.");
            }

            spellId = (short)reader.ReadVarUhShort();
            if (spellId < 0)
            {
                throw new System.Exception("Forbidden value (" + spellId + ") on element of AbstractFightDispellableEffect.spellId.");
            }

            effectId = (int)reader.ReadVarUhInt();
            if (effectId < 0)
            {
                throw new System.Exception("Forbidden value (" + effectId + ") on element of AbstractFightDispellableEffect.effectId.");
            }

            parentBoostUid = (int)reader.ReadVarUhInt();
            if (parentBoostUid < 0)
            {
                throw new System.Exception("Forbidden value (" + parentBoostUid + ") on element of AbstractFightDispellableEffect.parentBoostUid.");
            }

        }


    }
}








