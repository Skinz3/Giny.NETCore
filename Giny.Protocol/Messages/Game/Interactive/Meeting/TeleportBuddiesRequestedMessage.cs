using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class TeleportBuddiesRequestedMessage : NetworkMessage  
    { 
        public  const ushort Id = 2968;
        public override ushort MessageId => Id;

        public short dungeonId;
        public long inviterId;
        public long[] invalidBuddiesIds;

        public TeleportBuddiesRequestedMessage()
        {
        }
        public TeleportBuddiesRequestedMessage(short dungeonId,long inviterId,long[] invalidBuddiesIds)
        {
            this.dungeonId = dungeonId;
            this.inviterId = inviterId;
            this.invalidBuddiesIds = invalidBuddiesIds;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element dungeonId.");
            }

            writer.WriteVarShort((short)dungeonId);
            if (inviterId < 0 || inviterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + inviterId + ") on element inviterId.");
            }

            writer.WriteVarLong((long)inviterId);
            writer.WriteShort((short)invalidBuddiesIds.Length);
            for (uint _i3 = 0;_i3 < invalidBuddiesIds.Length;_i3++)
            {
                if (invalidBuddiesIds[_i3] < 0 || invalidBuddiesIds[_i3] > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + invalidBuddiesIds[_i3] + ") on element 3 (starting at 1) of invalidBuddiesIds.");
                }

                writer.WriteVarLong((long)invalidBuddiesIds[_i3]);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            double _val3 = double.NaN;
            dungeonId = (short)reader.ReadVarUhShort();
            if (dungeonId < 0)
            {
                throw new System.Exception("Forbidden value (" + dungeonId + ") on element of TeleportBuddiesRequestedMessage.dungeonId.");
            }

            inviterId = (long)reader.ReadVarUhLong();
            if (inviterId < 0 || inviterId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + inviterId + ") on element of TeleportBuddiesRequestedMessage.inviterId.");
            }

            uint _invalidBuddiesIdsLen = (uint)reader.ReadUShort();
            invalidBuddiesIds = new long[_invalidBuddiesIdsLen];
            for (uint _i3 = 0;_i3 < _invalidBuddiesIdsLen;_i3++)
            {
                _val3 = (double)reader.ReadVarUhLong();
                if (_val3 < 0 || _val3 > 9.00719925474099E+15)
                {
                    throw new System.Exception("Forbidden value (" + _val3 + ") on elements of invalidBuddiesIds.");
                }

                invalidBuddiesIds[_i3] = (long)_val3;
            }

        }


    }
}








