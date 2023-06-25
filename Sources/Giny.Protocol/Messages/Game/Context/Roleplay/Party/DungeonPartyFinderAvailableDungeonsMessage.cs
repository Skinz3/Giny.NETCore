using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class DungeonPartyFinderAvailableDungeonsMessage : NetworkMessage  
    { 
        public  const ushort Id = 1688;
        public override ushort MessageId => Id;

        public short[] dungeonIds;

        public DungeonPartyFinderAvailableDungeonsMessage()
        {
        }
        public DungeonPartyFinderAvailableDungeonsMessage(short[] dungeonIds)
        {
            this.dungeonIds = dungeonIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)dungeonIds.Length);
            for (uint _i1 = 0;_i1 < dungeonIds.Length;_i1++)
            {
                if (dungeonIds[_i1] < 0)
                {
                    throw new System.Exception("Forbidden value (" + dungeonIds[_i1] + ") on element 1 (starting at 1) of dungeonIds.");
                }

                writer.WriteVarShort((short)dungeonIds[_i1]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val1 = 0;
            uint _dungeonIdsLen = (uint)reader.ReadUShort();
            dungeonIds = new short[_dungeonIdsLen];
            for (uint _i1 = 0;_i1 < _dungeonIdsLen;_i1++)
            {
                _val1 = (uint)reader.ReadVarUhShort();
                if (_val1 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val1 + ") on elements of dungeonIds.");
                }

                dungeonIds[_i1] = (short)_val1;
            }

        }


    }
}








