using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class UpdatedStorageTabInformation
    {
        public const ushort Id = 1038;
        public virtual ushort TypeId => Id;

        public string name;
        public int tabNumber;
        public int picto;
        public int[] dropTypeLimitation;

        public UpdatedStorageTabInformation()
        {
        }
        public UpdatedStorageTabInformation(string name, int tabNumber, int picto, int[] dropTypeLimitation)
        {
            this.name = name;
            this.tabNumber = tabNumber;
            this.picto = picto;
            this.dropTypeLimitation = dropTypeLimitation;
        }
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF((string)name);
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element tabNumber.");
            }

            writer.WriteVarInt((int)tabNumber);
            if (picto < 0)
            {
                throw new System.Exception("Forbidden value (" + picto + ") on element picto.");
            }

            writer.WriteVarInt((int)picto);
            writer.WriteShort((short)dropTypeLimitation.Length);
            for (uint _i4 = 0; _i4 < dropTypeLimitation.Length; _i4++)
            {
                if (dropTypeLimitation[_i4] < 0)
                {
                    throw new System.Exception("Forbidden value (" + dropTypeLimitation[_i4] + ") on element 4 (starting at 1) of dropTypeLimitation.");
                }

                writer.WriteVarInt((int)dropTypeLimitation[_i4]);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val4 = 0;
            name = (string)reader.ReadUTF();
            tabNumber = (int)reader.ReadVarUhInt();
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element of UpdatedStorageTabInformation.tabNumber.");
            }

            picto = (int)reader.ReadVarUhInt();
            if (picto < 0)
            {
                throw new System.Exception("Forbidden value (" + picto + ") on element of UpdatedStorageTabInformation.picto.");
            }

            uint _dropTypeLimitationLen = (uint)reader.ReadUShort();
            dropTypeLimitation = new int[_dropTypeLimitationLen];
            for (uint _i4 = 0; _i4 < _dropTypeLimitationLen; _i4++)
            {
                _val4 = (uint)reader.ReadVarUhInt();
                if (_val4 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val4 + ") on elements of dropTypeLimitation.");
                }

                dropTypeLimitation[_i4] = (int)_val4;
            }

        }


    }
}


