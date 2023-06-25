using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DungeonPartyFinderRoomContentMessage : NetworkMessage  
    { 
        public  const ushort Id = 726;
        public override ushort MessageId => Id;

        public short dungeonId;
        public DungeonPartyFinderPlayer[] players;

        public DungeonPartyFinderRoomContentMessage()
        {
        }
        public DungeonPartyFinderRoomContentMessage(short dungeonId,DungeonPartyFinderPlayer[] players)
        {
            this.dungeonId = dungeonId;
            this.players = players;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element dungeonId.");
            }

            writer.WriteVarShort((short)dungeonId);
            writer.WriteShort((short)players.Length);
            for (uint _i2 = 0;_i2 < players.Length;_i2++)
            {
                (players[_i2] as DungeonPartyFinderPlayer).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            DungeonPartyFinderPlayer _item2 = null;
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of DungeonPartyFinderRoomContentMessage.dungeonId.");
            }

            uint _playersLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _playersLen;_i2++)
            {
                _item2 = new DungeonPartyFinderPlayer();
                _item2.Deserialize(reader);
                players[_i2] = _item2;
            }

        }


    }
}








