using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class UpdateMountCharacteristicsMessage : NetworkMessage  
    { 
        public  const ushort Id = 4691;
        public override ushort MessageId => Id;

        public int rideId;
        public UpdateMountCharacteristic[] boostToUpdateList;

        public UpdateMountCharacteristicsMessage()
        {
        }
        public UpdateMountCharacteristicsMessage(int rideId,UpdateMountCharacteristic[] boostToUpdateList)
        {
            this.rideId = rideId;
            this.boostToUpdateList = boostToUpdateList;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt((int)rideId);
            writer.WriteShort((short)boostToUpdateList.Length);
            for (uint _i2 = 0;_i2 < boostToUpdateList.Length;_i2++)
            {
                writer.WriteShort((short)(boostToUpdateList[_i2] as UpdateMountCharacteristic).TypeId);
                (boostToUpdateList[_i2] as UpdateMountCharacteristic).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id2 = 0;
            UpdateMountCharacteristic _item2 = null;
            rideId = (int)reader.ReadVarInt();
            uint _boostToUpdateListLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _boostToUpdateListLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<UpdateMountCharacteristic>((short)_id2);
                _item2.Deserialize(reader);
                boostToUpdateList[_i2] = _item2;
            }

        }


    }
}








