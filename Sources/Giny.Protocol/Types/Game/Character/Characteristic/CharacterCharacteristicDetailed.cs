using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class CharacterCharacteristicDetailed : CharacterCharacteristic
    {
        public new const ushort Id = 1174;
        public override ushort TypeId => Id;

        public int @base;
        public int additional;
        public int objectsAndMountBonus;
        public int alignGiftBonus;
        public int contextModif;

        public CharacterCharacteristicDetailed()
        {
        }
        public CharacterCharacteristicDetailed(int @base, int additional, int objectsAndMountBonus, int alignGiftBonus, int contextModif, short characteristicId)
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
            writer.WriteVarInt((int)@base);
            writer.WriteVarInt((int)additional);
            writer.WriteVarInt((int)objectsAndMountBonus);
            writer.WriteVarInt((int)alignGiftBonus);
            writer.WriteVarInt((int)contextModif);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            @base = (int)reader.ReadVarInt();
            additional = (int)reader.ReadVarInt();
            objectsAndMountBonus = (int)reader.ReadVarInt();
            alignGiftBonus = (int)reader.ReadVarInt();
            contextModif = (int)reader.ReadVarInt();
        }


    }
}


