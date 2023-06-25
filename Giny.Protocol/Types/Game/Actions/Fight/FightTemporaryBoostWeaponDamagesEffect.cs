using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightTemporaryBoostWeaponDamagesEffect : FightTemporaryBoostEffect  
    { 
        public new const ushort Id = 542;
        public override ushort TypeId => Id;

        public short weaponTypeId;

        public FightTemporaryBoostWeaponDamagesEffect()
        {
        }
        public FightTemporaryBoostWeaponDamagesEffect(short weaponTypeId,int uid,double targetId,short turnDuration,byte dispelable,short spellId,int effectId,int parentBoostUid,int delta)
        {
            this.weaponTypeId = weaponTypeId;
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
            writer.WriteShort((short)weaponTypeId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            weaponTypeId = (short)reader.ReadShort();
        }


    }
}








