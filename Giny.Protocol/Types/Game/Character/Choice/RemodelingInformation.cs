using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class RemodelingInformation  
    { 
        public const ushort Id = 480;
        public virtual ushort TypeId => Id;

        public string name;
        public byte breed;
        public bool sex;
        public short cosmeticId;
        public int[] colors;

        public RemodelingInformation()
        {
        }
        public RemodelingInformation(string name,byte breed,bool sex,short cosmeticId,int[] colors)
        {
            this.name = name;
            this.breed = breed;
            this.sex = sex;
            this.cosmeticId = cosmeticId;
            this.colors = colors;
        }
        public virtual void Serialize(IDataWriter writer)
        {
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
        public virtual void Deserialize(IDataReader reader)
        {
            int _val5 = 0;
            name = (string)reader.ReadUTF();
            breed = (byte)reader.ReadByte();
            sex = (bool)reader.ReadBoolean();
            cosmeticId = (short)reader.ReadVarUhShort();
            if (cosmeticId < 0)
            {
                throw new System.Exception("Forbidden value (" + cosmeticId + ") on element of RemodelingInformation.cosmeticId.");
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








