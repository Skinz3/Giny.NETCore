using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeBidHouseListMessage : NetworkMessage
    {
        public const ushort Id = 9324;
        public override ushort MessageId => Id;

        public int objectGID;
        public bool follow;

        public ExchangeBidHouseListMessage()
        {
        }
        public ExchangeBidHouseListMessage(int objectGID, bool follow)
        {
            this.objectGID = objectGID;
            this.follow = follow;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element objectGID.");
            }

            writer.WriteVarInt((int)objectGID);
            writer.WriteBoolean((bool)follow);
        }
        public override void Deserialize(IDataReader reader)
        {
            objectGID = (int)reader.ReadVarUhInt();
            if (objectGID < 0)
            {
                throw new System.Exception("Forbidden value (" + objectGID + ") on element of ExchangeBidHouseListMessage.objectGID.");
            }

            follow = (bool)reader.ReadBoolean();
        }

    }
}


