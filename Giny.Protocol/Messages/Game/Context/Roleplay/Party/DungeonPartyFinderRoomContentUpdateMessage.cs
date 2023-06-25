using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DungeonPartyFinderRoomContentUpdateMessage : NetworkMessage  
    { 
        public  const ushort Id = 7648;
        public override ushort MessageId => Id;

        public short dungeonId;
        public DungeonPartyFinderPlayer[] addedPlayers;
        public long[] removedPlayersIds;

        public DungeonPartyFinderRoomContentUpdateMessage()
        {
        }
        public DungeonPartyFinderRoomContentUpdateMessage(short dungeonId,DungeonPartyFinderPlayer[] addedPlayers,long[] removedPlayersIds)
        {
            this.dungeonId = dungeonId;
            this.addedPlayers = addedPlayers;
            this.removedPlayersIds = removedPlayersIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element dungeonId.");
            }

            writer.WriteVarShort((short)dungeonId);
            writer.WriteShort((short)addedPlayers.Length);
            for (uint _i2 = 0;_i2 < addedPlayers.Length;_i2++)
            {
                (addedPlayers[_i2] as DungeonPartyFinderPlayer).Serialize(writer);
            }

            writer.WriteShort((short)removedPlayersIds.Length);
            for (uint _i3 = 0;_i3 < removedPlayersIds.Length;_i3++)
            {
                if (removedPlayersIds[_i3] < 0 || removedPlayersIds[_i3] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + removedPlayersIds[_i3] + ") on element 3 (starting at 1) of removedPlayersIds.");
                }

                writer.WriteVarLong((long)removedPlayersIds[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            DungeonPartyFinderPlayer _item2 = null;
            double _val3 = double.NaN;
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of DungeonPartyFinderRoomContentUpdateMessage.dungeonId.");
            }

            uint _addedPlayersLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _addedPlayersLen;_i2++)
            {
                _item2 = new DungeonPartyFinderPlayer();
                _item2.Deserialize(reader);
                addedPlayers[_i2] = _item2;
            }

            uint _removedPlayersIdsLen = (uint)reader.ReadUShort();
            removedPlayersIds = new long[_removedPlayersIdsLen];
            for (uint _i3 = 0;_i3 < _removedPlayersIdsLen;_i3++)
            {
                _val3 = (double)reader.ReadVarUhLong();
                if (_val3 < 0 || _val3 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of removedPlayersIds.");
                }

                removedPlayersIds[_i3] = (long)_val3;
            }

        }


    }
}








