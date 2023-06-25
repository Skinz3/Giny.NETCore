using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class PartyFollowStatusUpdateMessage : AbstractPartyMessage  
    { 
        public new const ushort Id = 1757;
        public override ushort MessageId => Id;

        public bool success;
        public bool isFollowed;
        public long followedId;

        public PartyFollowStatusUpdateMessage()
        {
        }
        public PartyFollowStatusUpdateMessage(bool success,bool isFollowed,long followedId,int partyId)
        {
            this.success = success;
            this.isFollowed = isFollowed;
            this.followedId = followedId;
            this.partyId = partyId;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,success);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,isFollowed);
            writer.WriteByte((byte)_box0);
            if (followedId < 0 || followedId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + followedId + ") on element followedId.");
            }

            writer.WriteVarLong((long)followedId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            byte _box0 = reader.ReadByte();
            success = BooleanByteWrapper.GetFlag(_box0,0);
            isFollowed = BooleanByteWrapper.GetFlag(_box0,1);
            followedId = (long)reader.ReadVarUhLong();
            if (followedId < 0 || followedId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + followedId + ") on element of PartyFollowStatusUpdateMessage.followedId.");
            }

        }


    }
}








