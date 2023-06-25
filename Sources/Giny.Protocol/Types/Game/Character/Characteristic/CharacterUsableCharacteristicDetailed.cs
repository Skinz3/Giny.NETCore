using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterUsableCharacteristicDetailed : CharacterCharacteristicDetailed  
    { 
        public new const ushort Id = 3142;
        public override ushort TypeId => Id;

        public short used;

        public CharacterUsableCharacteristicDetailed()
        {
        }
        public CharacterUsableCharacteristicDetailed(short used,short characteristicId,short @base,short additional,short objectsAndMountBonus,short alignGiftBonus,short contextModif)
        {
            this.used = used;
            this.characteristicId = characteristicId;
            this.@base = @base;
            this.additional = additional;
            this.objectsAndMountBonus = objectsAndMountBonus;
            this.alignGiftBonus = alignGiftBonus;
            this.contextModif = contextModif;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (used < 0)
            {
                throw new System.Exception("Forbidden value (" + used + ") on element used.");
            }

            writer.WriteVarShort((short)used);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            used = (short)reader.ReadVarUhShort();
            if (used < 0)
            {
                throw new System.Exception("Forbidden value (" + used + ") on element of CharacterUsableCharacteristicDetailed.used.");
            }

        }


    }
}








