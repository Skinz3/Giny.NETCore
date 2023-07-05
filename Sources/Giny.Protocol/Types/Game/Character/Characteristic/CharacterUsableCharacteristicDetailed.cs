using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class CharacterUsableCharacteristicDetailed : CharacterCharacteristicDetailed
    {
        public new const ushort Id = 1123;
        public override ushort TypeId => Id;

        public int used;

        public CharacterUsableCharacteristicDetailed()
        {
        }
        public CharacterUsableCharacteristicDetailed(int used, short characteristicId, int @base, int additional, int objectsAndMountBonus, int alignGiftBonus, int contextModif)
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

            writer.WriteVarInt((int)used);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            used = (int)reader.ReadVarUhInt();
            if (used < 0)
            {
                throw new System.Exception("Forbidden value (" + used + ") on element of CharacterUsableCharacteristicDetailed.used.");
            }

        }


    }
}


