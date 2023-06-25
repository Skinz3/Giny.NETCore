using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class HousePropertiesMessage : NetworkMessage  
    { 
        public  const ushort Id = 1755;
        public override ushort MessageId => Id;

        public int houseId;
        public int[] doorsOnMap;
        public HouseInstanceInformations properties;

        public HousePropertiesMessage()
        {
        }
        public HousePropertiesMessage(int houseId,int[] doorsOnMap,HouseInstanceInformations properties)
        {
            this.houseId = houseId;
            this.doorsOnMap = doorsOnMap;
            this.properties = properties;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element houseId.");
            }

            writer.WriteVarInt((int)houseId);
            writer.WriteShort((short)doorsOnMap.Length);
            for (uint _i2 = 0;_i2 < doorsOnMap.Length;_i2++)
            {
                if (doorsOnMap[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + doorsOnMap[_i2] + ") on element 2 (starting at 1) of doorsOnMap.");
                }

                writer.WriteInt((int)doorsOnMap[_i2]);
            }

            writer.WriteShort((short)properties.TypeId);
            properties.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            houseId = (int)reader.ReadVarUhInt();
            if (houseId < 0)
            {
                throw new System.Exception("Forbidden value (" + houseId + ") on element of HousePropertiesMessage.houseId.");
            }

            uint _doorsOnMapLen = (uint)reader.ReadUShort();
            doorsOnMap = new int[_doorsOnMapLen];
            for (uint _i2 = 0;_i2 < _doorsOnMapLen;_i2++)
            {
                _val2 = (uint)reader.ReadInt();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of doorsOnMap.");
                }

                doorsOnMap[_i2] = (int)_val2;
            }

            uint _id3 = (uint)reader.ReadUShort();
            properties = ProtocolTypeManager.GetInstance<HouseInstanceInformations>((short)_id3);
            properties.Deserialize(reader);
        }


    }
}








