using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TeleportDestinationsMessage : NetworkMessage  
    { 
        public  const ushort Id = 6883;
        public override ushort MessageId => Id;

        public byte type;
        public TeleportDestination[] destinations;

        public TeleportDestinationsMessage()
        {
        }
        public TeleportDestinationsMessage(byte type,TeleportDestination[] destinations)
        {
            this.type = type;
            this.destinations = destinations;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)type);
            writer.WriteShort((short)destinations.Length);
            for (uint _i2 = 0;_i2 < destinations.Length;_i2++)
            {
                (destinations[_i2] as TeleportDestination).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            TeleportDestination _item2 = null;
            type = (byte)reader.ReadByte();
            if (type < 0)
            {
                throw new System.Exception("Forbidden value (" + type + ") on element of TeleportDestinationsMessage.type.");
            }

            uint _destinationsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _destinationsLen;_i2++)
            {
                _item2 = new TeleportDestination();
                _item2.Deserialize(reader);
                destinations[_i2] = _item2;
            }

        }


    }
}








