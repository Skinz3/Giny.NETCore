using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTemporaryBoostEffect : AbstractFightDispellableEffect  
    { 
        public new const ushort Id = 4972;
        public override ushort TypeId => Id;

        public int delta;

        public FightTemporaryBoostEffect()
        {
        }
        public FightTemporaryBoostEffect(int delta,int uid,double targetId,short turnDuration,byte dispelable,short spellId,int effectId,int parentBoostUid)
        {
            this.delta = delta;
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
            writer.WriteInt((int)delta);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            delta = (int)reader.ReadInt();
        }


    }
}








