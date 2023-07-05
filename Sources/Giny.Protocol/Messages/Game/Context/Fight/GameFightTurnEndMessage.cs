using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class GameFightTurnEndMessage : NetworkMessage
    {
        public const ushort Id = 6718;
        public override ushort MessageId => Id;

        public double id;

        public GameFightTurnEndMessage()
        {
        }
        public GameFightTurnEndMessage(double id)
        {
            this.id = id;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < -9007199254740992 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (double)reader.ReadDouble();
            if (id < -9007199254740992 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of GameFightTurnEndMessage.id.");
            }

        }

    }
}


