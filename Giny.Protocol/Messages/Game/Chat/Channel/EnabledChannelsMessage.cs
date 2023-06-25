using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class EnabledChannelsMessage : NetworkMessage  
    { 
        public  const ushort Id = 9741;
        public override ushort MessageId => Id;

        public byte[] channels;
        public byte[] disallowed;

        public EnabledChannelsMessage()
        {
        }
        public EnabledChannelsMessage(byte[] channels,byte[] disallowed)
        {
            this.channels = channels;
            this.disallowed = disallowed;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)channels.Length);
            for (uint _i1 = 0;_i1 < channels.Length;_i1++)
            {
                writer.WriteByte((byte)channels[_i1]);
            }

            writer.WriteShort((short)disallowed.Length);
            for (uint _i2 = 0;_i2 < disallowed.Length;_i2++)
            {
                writer.WriteByte((byte)disallowed[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _val2 = 0;
            uint _channelsLen = (uint)reader.ReadUShort();
            channels = new byte[_channelsLen];
            for (uint _i1 = 0;_i1 < _channelsLen;_i1++)
            {
                _val1 = (uint)reader.ReadByte();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of channels.");
                }

                channels[_i1] = (byte)_val1;
            }

            uint _disallowedLen = (uint)reader.ReadUShort();
            disallowed = new byte[_disallowedLen];
            for (uint _i2 = 0;_i2 < _disallowedLen;_i2++)
            {
                _val2 = (uint)reader.ReadByte();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of disallowed.");
                }

                disallowed[_i2] = (byte)_val2;
            }

        }


    }
}








