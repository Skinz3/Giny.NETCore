using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class EntityLook  
    { 
        public const ushort Id = 212;
        public virtual ushort TypeId => Id;

        public short bonesId;
        public short[] skins;
        public int[] indexedColors;
        public short[] scales;
        public SubEntity[] subentities;

        public EntityLook()
        {
        }
        public EntityLook(short bonesId,short[] skins,int[] indexedColors,short[] scales,SubEntity[] subentities)
        {
            this.bonesId = bonesId;
            this.skins = skins;
            this.indexedColors = indexedColors;
            this.scales = scales;
            this.subentities = subentities;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            if (bonesId < 0)
            {
                throw new System.Exception("Forbidden value (" + bonesId + ") on element bonesId.");
            }

            writer.WriteVarShort((short)bonesId);
            writer.WriteShort((short)skins.Length);
            for (uint _i2 = 0;_i2 < skins.Length;_i2++)
            {
                if (skins[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + skins[_i2] + ") on element 2 (starting at 1) of skins.");
                }

                writer.WriteVarShort((short)skins[_i2]);
            }

            writer.WriteShort((short)indexedColors.Length);
            for (uint _i3 = 0;_i3 < indexedColors.Length;_i3++)
            {
                writer.WriteInt((int)indexedColors[_i3]);
            }

            writer.WriteShort((short)scales.Length);
            for (uint _i4 = 0;_i4 < scales.Length;_i4++)
            {
                writer.WriteVarShort((short)scales[_i4]);
            }

            writer.WriteShort((short)subentities.Length);
            for (uint _i5 = 0;_i5 < subentities.Length;_i5++)
            {
                (subentities[_i5] as SubEntity).Serialize(writer);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            int _val3 = 0;
            int _val4 = 0;
            SubEntity _item5 = null;
            bonesId = (short)reader.ReadVarUhShort();
            if (bonesId < 0)
            {
                throw new System.Exception("Forbidden value (" + bonesId + ") on element of EntityLook.bonesId.");
            }

            uint _skinsLen = (uint)reader.ReadUShort();
            skins = new short[_skinsLen];
            for (uint _i2 = 0;_i2 < _skinsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of skins.");
                }

                skins[_i2] = (short)_val2;
            }

            uint _indexedColorsLen = (uint)reader.ReadUShort();
            indexedColors = new int[_indexedColorsLen];
            for (uint _i3 = 0;_i3 < _indexedColorsLen;_i3++)
            {
                _val3 = (int)reader.ReadInt();
                indexedColors[_i3] = (int)_val3;
            }

            uint _scalesLen = (uint)reader.ReadUShort();
            scales = new short[_scalesLen];
            for (uint _i4 = 0;_i4 < _scalesLen;_i4++)
            {
                _val4 = (int)reader.ReadVarShort();
                scales[_i4] = (short)_val4;
            }

            uint _subentitiesLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _subentitiesLen;_i5++)
            {
                _item5 = new SubEntity();
                _item5.Deserialize(reader);
                subentities[_i5] = _item5;
            }

        }


    }
}








