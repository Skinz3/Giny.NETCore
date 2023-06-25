using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterToRemodelInformations : CharacterRemodelingInformation  
    { 
        public new const ushort Id = 2999;
        public override ushort TypeId => Id;

        public byte possibleChangeMask;
        public byte mandatoryChangeMask;

        public CharacterToRemodelInformations()
        {
        }
        public CharacterToRemodelInformations(byte possibleChangeMask,byte mandatoryChangeMask,long id,string name,byte breed,bool sex,short cosmeticId,int[] colors)
        {
            this.possibleChangeMask = possibleChangeMask;
            this.mandatoryChangeMask = mandatoryChangeMask;
            this.id = id;
            this.name = name;
            this.breed = breed;
            this.sex = sex;
            this.cosmeticId = cosmeticId;
            this.colors = colors;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (possibleChangeMask < 0)
            {
                throw new System.Exception("Forbidden value (" + possibleChangeMask + ") on element possibleChangeMask.");
            }

            writer.WriteByte((byte)possibleChangeMask);
            if (mandatoryChangeMask < 0)
            {
                throw new System.Exception("Forbidden value (" + mandatoryChangeMask + ") on element mandatoryChangeMask.");
            }

            writer.WriteByte((byte)mandatoryChangeMask);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            possibleChangeMask = (byte)reader.ReadByte();
            if (possibleChangeMask < 0)
            {
                throw new System.Exception("Forbidden value (" + possibleChangeMask + ") on element of CharacterToRemodelInformations.possibleChangeMask.");
            }

            mandatoryChangeMask = (byte)reader.ReadByte();
            if (mandatoryChangeMask < 0)
            {
                throw new System.Exception("Forbidden value (" + mandatoryChangeMask + ") on element of CharacterToRemodelInformations.mandatoryChangeMask.");
            }

        }


    }
}








