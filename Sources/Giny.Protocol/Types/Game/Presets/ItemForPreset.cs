using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class ItemForPreset
    {
        public const ushort Id = 5762;
        public virtual ushort TypeId => Id;

        public short position;
        public int objGid;
        public int objUid;

        public ItemForPreset()
        {
        }
        public ItemForPreset(short position, int objGid, int objUid)
        {
            this.position = position;
            this.objGid = objGid;
            this.objUid = objUid;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)position);
            if (objGid < 0)
            {
                throw new System.Exception("Forbidden value (" + objGid + ") on element objGid.");
            }

            writer.WriteVarInt((int)objGid);
            if (objUid < 0)
            {
                throw new System.Exception("Forbidden value (" + objUid + ") on element objUid.");
            }

            writer.WriteVarInt((int)objUid);
        }
        public virtual void Deserialize(IDataReader reader)
        {
            position = (short)reader.ReadShort();
            if (position < 0)
            {
                throw new System.Exception("Forbidden value (" + position + ") on element of ItemForPreset.position.");
            }

            objGid = (int)reader.ReadVarUhInt();
            if (objGid < 0)
            {
                throw new System.Exception("Forbidden value (" + objGid + ") on element of ItemForPreset.objGid.");
            }

            objUid = (int)reader.ReadVarUhInt();
            if (objUid < 0)
            {
                throw new System.Exception("Forbidden value (" + objUid + ") on element of ItemForPreset.objUid.");
            }

        }


    }
}


