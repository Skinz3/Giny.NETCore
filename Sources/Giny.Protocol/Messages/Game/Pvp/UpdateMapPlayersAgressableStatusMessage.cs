using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class UpdateMapPlayersAgressableStatusMessage : NetworkMessage  
    { 
        public  const ushort Id = 4601;
        public override ushort MessageId => Id;

        public long[] playerIds;
        public byte[] enable;

        public UpdateMapPlayersAgressableStatusMessage()
        {
        }
        public UpdateMapPlayersAgressableStatusMessage(long[] playerIds,byte[] enable)
        {
            this.playerIds = playerIds;
            this.enable = enable;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)playerIds.Length);
            for (uint _i1 = 0;_i1 < playerIds.Length;_i1++)
            {
                if (playerIds[_i1] < 0 || playerIds[_i1] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + playerIds[_i1] + ") on element 1 (starting at 1) of playerIds.");
                }

                writer.WriteVarLong((long)playerIds[_i1]);
            }

            writer.WriteShort((short)enable.Length);
            for (uint _i2 = 0;_i2 < enable.Length;_i2++)
            {
                writer.WriteByte((byte)enable[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val1 = double.NaN;
            uint _val2 = 0;
            uint _playerIdsLen = (uint)reader.ReadUShort();
            playerIds = new long[_playerIdsLen];
            for (uint _i1 = 0;_i1 < _playerIdsLen;_i1++)
            {
                _val1 = (double)reader.ReadVarUhLong();
                if (_val1 < 0 || _val1 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of playerIds.");
                }

                playerIds[_i1] = (long)_val1;
            }

            uint _enableLen = (uint)reader.ReadUShort();
            enable = new byte[_enableLen];
            for (uint _i2 = 0;_i2 < _enableLen;_i2++)
            {
                _val2 = (uint)reader.ReadByte();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of enable.");
                }

                enable[_i2] = (byte)_val2;
            }

        }


    }
}








