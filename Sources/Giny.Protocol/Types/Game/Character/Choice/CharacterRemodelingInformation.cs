using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class CharacterRemodelingInformation : AbstractCharacterInformation  
    { 
        public new const ushort Id = 8348;
        public override ushort TypeId => Id;

        public string name;
        public byte breed;
        public bool sex;
        public short cosmeticId;
        public int[] colors;

        public CharacterRemodelingInformation()
        {
        }
        public CharacterRemodelingInformation(string name,byte breed,bool sex,short cosmeticId,int[] colors,long id)
        {
            this.name = name;
            this.breed = breed;
            this.sex = sex;
            this.cosmeticId = cosmeticId;
            this.colors = colors;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)name);
            writer.WriteByte((byte)breed);
            writer.WriteBoolean((bool)sex);
            if (cosmeticId < 0)
            {
                throw new System.Exception("Forbidden value (" + cosmeticId + ") on element cosmeticId.");
            }

            writer.WriteVarShort((short)cosmeticId);
            writer.WriteShort((short)colors.Length);
            for (uint _i5 = 0;_i5 < colors.Length;_i5++)
            {
                writer.WriteInt((int)colors[_i5]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            int _val5 = 0;
            base.Deserialize(reader);
            name = (string)reader.ReadUTF();
            breed = (byte)reader.ReadByte();
            sex = (bool)reader.ReadBoolean();
            cosmeticId = (short)reader.ReadVarUhShort();
            if (cosmeticId < 0)
            {
                throw new System.Exception("Forbidden value (" + cosmeticId + ") on element of CharacterRemodelingInformation.cosmeticId.");
            }

            uint _colorsLen = (uint)reader.ReadUShort();
            colors = new int[_colorsLen];
            for (uint _i5 = 0;_i5 < _colorsLen;_i5++)
            {
                _val5 = (int)reader.ReadInt();
                colors[_i5] = (int)_val5;
            }

        }


    }
}








