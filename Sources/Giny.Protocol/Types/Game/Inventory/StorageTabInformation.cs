using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{
    public class StorageTabInformation
    {
        public const ushort Id = 3527;
        public virtual ushort TypeId => Id;

        public string name;
        public int tabNumber;
        public int picto;
        public int openRight;
        public int dropRight;
        public int takeRight;
        public int[] dropTypeLimitation;

        public StorageTabInformation()
        {
        }
        public StorageTabInformation(string name, int tabNumber, int picto, int openRight, int dropRight, int takeRight, int[] dropTypeLimitation)
        {
            this.name = name;
            this.tabNumber = tabNumber;
            this.picto = picto;
            this.openRight = openRight;
            this.dropRight = dropRight;
            this.takeRight = takeRight;
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
            if (openRight < 0)
            {
                throw new System.Exception("Forbidden value (" + openRight + ") on element openRight.");
            }

            writer.WriteVarInt((int)openRight);
            if (dropRight < 0)
            {
                throw new System.Exception("Forbidden value (" + dropRight + ") on element dropRight.");
            }

            writer.WriteVarInt((int)dropRight);
            if (takeRight < 0)
            {
                throw new System.Exception("Forbidden value (" + takeRight + ") on element takeRight.");
            }

            writer.WriteVarInt((int)takeRight);
            writer.WriteShort((short)dropTypeLimitation.Length);
            for (uint _i7 = 0; _i7 < dropTypeLimitation.Length; _i7++)
            {
                if (dropTypeLimitation[_i7] < 0)
                {
                    throw new System.Exception("Forbidden value (" + dropTypeLimitation[_i7] + ") on element 7 (starting at 1) of dropTypeLimitation.");
                }

                writer.WriteVarInt((int)dropTypeLimitation[_i7]);
            }

        }
        public virtual void Deserialize(IDataReader reader)
        {
            uint _val7 = 0;
            name = (string)reader.ReadUTF();
            tabNumber = (int)reader.ReadVarUhInt();
            if (tabNumber < 0)
            {
                throw new System.Exception("Forbidden value (" + tabNumber + ") on element of StorageTabInformation.tabNumber.");
            }

            picto = (int)reader.ReadVarUhInt();
            if (picto < 0)
            {
                throw new System.Exception("Forbidden value (" + picto + ") on element of StorageTabInformation.picto.");
            }

            openRight = (int)reader.ReadVarUhInt();
            if (openRight < 0)
            {
                throw new System.Exception("Forbidden value (" + openRight + ") on element of StorageTabInformation.openRight.");
            }

            dropRight = (int)reader.ReadVarUhInt();
            if (dropRight < 0)
            {
                throw new System.Exception("Forbidden value (" + dropRight + ") on element of StorageTabInformation.dropRight.");
            }

            takeRight = (int)reader.ReadVarUhInt();
            if (takeRight < 0)
            {
                throw new System.Exception("Forbidden value (" + takeRight + ") on element of StorageTabInformation.takeRight.");
            }

            uint _dropTypeLimitationLen = (uint)reader.ReadUShort();
            dropTypeLimitation = new int[_dropTypeLimitationLen];
            for (uint _i7 = 0; _i7 < _dropTypeLimitationLen; _i7++)
            {
                _val7 = (uint)reader.ReadVarUhInt();
                if (_val7 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val7 + ") on elements of dropTypeLimitation.");
                }

                dropTypeLimitation[_i7] = (int)_val7;
            }

        }


    }
}


