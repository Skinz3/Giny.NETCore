using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class UpdateGuildRightsMessage : NetworkMessage  
    { 
        public  const ushort Id = 6998;
        public override ushort MessageId => Id;

        public int rankId;
        public int[] rights;

        public UpdateGuildRightsMessage()
        {
        }
        public UpdateGuildRightsMessage(int rankId,int[] rights)
        {
            this.rankId = rankId;
            this.rights = rights;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element rankId.");
            }

            writer.WriteVarInt((int)rankId);
            writer.WriteShort((short)rights.Length);
            for (uint _i2 = 0;_i2 < rights.Length;_i2++)
            {
                if (rights[_i2] < 0)
                {
                    throw new System.Exception("Forbidden value (" + rights[_i2] + ") on element 2 (starting at 1) of rights.");
                }

                writer.WriteVarInt((int)rights[_i2]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _val2 = 0;
            rankId = (int)reader.ReadVarUhInt();
            if (rankId < 0)
            {
                throw new System.Exception("Forbidden value (" + rankId + ") on element of UpdateGuildRightsMessage.rankId.");
            }

            uint _rightsLen = (uint)reader.ReadUShort();
            rights = new int[_rightsLen];
            for (uint _i2 = 0;_i2 < _rightsLen;_i2++)
            {
                _val2 = (uint)reader.ReadVarUhInt();
                if (_val2 < 0)
                {
                    throw new System.Exception("Forbidden value (" + _val2 + ") on elements of rights.");
                }

                rights[_i2] = (int)_val2;
            }

        }


    }
}








