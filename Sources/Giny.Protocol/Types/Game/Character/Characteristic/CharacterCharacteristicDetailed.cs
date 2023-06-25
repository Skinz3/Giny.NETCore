using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterCharacteristicDetailed : CharacterCharacteristic  
    { 
        public new const ushort Id = 8403;
        public override ushort TypeId => Id;

        public short @base;
        public short additional;
        public short objectsAndMountBonus;
        public short alignGiftBonus;
        public short contextModif;

        public CharacterCharacteristicDetailed()
        {
        }
        public CharacterCharacteristicDetailed(short @base,short additional,short objectsAndMountBonus,short alignGiftBonus,short contextModif,short characteristicId)
        {
            this.@base = @base;
            this.additional = additional;
            this.objectsAndMountBonus = objectsAndMountBonus;
            this.alignGiftBonus = alignGiftBonus;
            this.contextModif = contextModif;
            this.characteristicId = characteristicId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort((short)@base);
            writer.WriteVarShort((short)additional);
            writer.WriteVarShort((short)objectsAndMountBonus);
            writer.WriteVarShort((short)alignGiftBonus);
            writer.WriteVarShort((short)contextModif);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            @base = (short)reader.ReadVarShort();
            additional = (short)reader.ReadVarShort();
            objectsAndMountBonus = (short)reader.ReadVarShort();
            alignGiftBonus = (short)reader.ReadVarShort();
            contextModif = (short)reader.ReadVarShort();
        }


    }
}








