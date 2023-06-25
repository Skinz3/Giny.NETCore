using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class IdolsPreset : Preset  
    { 
        public new const ushort Id = 2034;
        public override ushort TypeId => Id;

        public short iconId;
        public short[] idolIds;

        public IdolsPreset()
        {
        }
        public IdolsPreset(short iconId,short[] idolIds,short id)
        {
            this.iconId = iconId;
            this.idolIds = idolIds;
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (iconId < 0)
            {
                throw new System.Exception("Forbidden value (" + iconId + ") on element iconId.");
            }

            writer.WriteShort((short)iconId);
            writer.WriteShort((short)idolIds.Length);
            for (uint _i2 = 0;_i2 < idolIds.Length;_i2++)
            {
                if (idolIds[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + idolIds[_i2] + ") on element 2 (starting at 1) of idolIds.");
                }

                writer.WriteVarShort((short)idolIds[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            base.Deserialize(reader);
            iconId = (short)reader.ReadShort();
            if (iconId < 0)
            {
                throw new System.Exception("Forbidden value (" + iconId + ") on element of IdolsPreset.iconId.");
            }

            uint _idolIdsLen = (uint)reader.ReadUShort();
            idolIds = new short[_idolIdsLen];
            for (uint _i2 = 0;_i2 < _idolIdsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhShort();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of idolIds.");
                }

                idolIds[_i2] = (short)_val2;
            }

        }


    }
}








