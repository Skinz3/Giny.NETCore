using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTemporarySpellBoostEffect : FightTemporaryBoostEffect  
    { 
        public new const ushort Id = 3626;
        public override ushort TypeId => Id;

        public short boostedSpellId;

        public FightTemporarySpellBoostEffect()
        {
        }
        public FightTemporarySpellBoostEffect(short boostedSpellId,int uid,double targetId,short turnDuration,byte dispelable,short spellId,int effectId,int parentBoostUid,int delta)
        {
            this.boostedSpellId = boostedSpellId;
            this.uid = uid;
            this.targetId = targetId;
            this.turnDuration = turnDuration;
            this.dispelable = dispelable;
            this.spellId = spellId;
            this.effectId = effectId;
            this.parentBoostUid = parentBoostUid;
            this.delta = delta;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (boostedSpellId < 0)
            {
                throw new System.Exception("Forbidden value (" + boostedSpellId + ") on element boostedSpellId.");
            }

            writer.WriteVarShort((short)boostedSpellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            boostedSpellId = (short)reader.ReadVarUhShort();
            if (boostedSpellId < 0)
            {
                throw new System.Exception("Forbidden value (" + boostedSpellId + ") on element of FightTemporarySpellBoostEffect.boostedSpellId.");
            }

        }


    }
}








