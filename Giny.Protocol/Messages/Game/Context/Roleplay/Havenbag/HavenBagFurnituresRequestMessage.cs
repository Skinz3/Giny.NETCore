using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HavenBagFurnituresRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 9809;
        public override ushort MessageId => Id;

        public short[] cellIds;
        public int[] funitureIds;
        public byte[] orientations;

        public HavenBagFurnituresRequestMessage()
        {
        }
        public HavenBagFurnituresRequestMessage(short[] cellIds,int[] funitureIds,byte[] orientations)
        {
            this.cellIds = cellIds;
            this.funitureIds = funitureIds;
            this.orientations = orientations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)cellIds.Length);
            for (uint _i1 = 0;_i1 < cellIds.Length;_i1++)
            {
                if (cellIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + cellIds[_i1] + ") on element 1 (starting at 1) of cellIds.");
                }

                writer.WriteVarShort((short)cellIds[_i1]);
            }

            writer.WriteShort((short)funitureIds.Length);
            for (uint _i2 = 0;_i2 < funitureIds.Length;_i2++)
            {
                writer.WriteInt((int)funitureIds[_i2]);
            }

            writer.WriteShort((short)orientations.Length);
            for (uint _i3 = 0;_i3 < orientations.Length;_i3++)
            {
                if (orientations[_i3] < 0)
                {
                    throw new System.Exception("Forbidden value (" + orientations[_i3] + ") on element 3 (starting at 1) of orientations.");
                }

                writer.WriteByte((byte)orientations[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            int _val2 = 0;
            uint _val3 = 0;
            uint _cellIdsLen = (uint)reader.ReadUShort();
            cellIds = new short[_cellIdsLen];
            for (uint _i1 = 0;_i1 < _cellIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of cellIds.");
                }

                cellIds[_i1] = (short)_val1;
            }

            uint _funitureIdsLen = (uint)reader.ReadUShort();
            funitureIds = new int[_funitureIdsLen];
            for (uint _i2 = 0;_i2 < _funitureIdsLen;_i2++)
            {
                _val2 = (int)reader.ReadInt();
                funitureIds[_i2] = (int)_val2;
            }

            uint _orientationsLen = (uint)reader.ReadUShort();
            orientations = new byte[_orientationsLen];
            for (uint _i3 = 0;_i3 < _orientationsLen;_i3++)
            {
                _val3 = (uint)reader.ReadByte();
                if (_val3 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of orientations.");
                }

                orientations[_i3] = (byte)_val3;
            }

        }


    }
}








