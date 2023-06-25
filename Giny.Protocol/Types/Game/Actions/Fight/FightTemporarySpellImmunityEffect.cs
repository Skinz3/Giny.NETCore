using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTemporarySpellImmunityEffect : AbstractFightDispellableEffect  
    { 
        public new const ushort Id = 1925;
        public override ushort TypeId => Id;

        public int immuneSpellId;

        public FightTemporarySpellImmunityEffect()
        {
        }
        public FightTemporarySpellImmunityEffect(int immuneSpellId,int uid,double targetId,short turnDuration,byte dispelable,short spellId,int effectId,int parentBoostUid)
        {
            this.immuneSpellId = immuneSpellId;
            this.uid = uid;
            this.targetId = targetId;
            this.turnDuration = turnDuration;
            this.dispelable = dispelable;
            this.spellId = spellId;
            this.effectId = effectId;
            this.parentBoostUid = parentBoostUid;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt((int)immuneSpellId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            immuneSpellId = (int)reader.ReadInt();
        }


    }
}








