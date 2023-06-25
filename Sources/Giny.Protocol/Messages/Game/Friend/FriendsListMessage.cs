using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FriendsListMessage : NetworkMessage  
    { 
        public  const ushort Id = 4381;
        public override ushort MessageId => Id;

        public FriendInformations[] friendsList;

        public FriendsListMessage()
        {
        }
        public FriendsListMessage(FriendInformations[] friendsList)
        {
            this.friendsList = friendsList;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)friendsList.Length);
            for (uint _i1 = 0;_i1 < friendsList.Length;_i1++)
            {
                writer.WriteShort((short)(friendsList[_i1] as FriendInformations).TypeId);
                (friendsList[_i1] as FriendInformations).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            FriendInformations _item1 = null;
            uint _friendsListLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _friendsListLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<FriendInformations>((short)_id1);
                _item1.Deserialize(reader);
                friendsList[_i1] = _item1;
            }

        }


    }
}








