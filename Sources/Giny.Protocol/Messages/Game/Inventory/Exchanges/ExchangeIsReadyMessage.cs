using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ExchangeIsReadyMessage : NetworkMessage
    {
        public const ushort Id = 5230;
        public override ushort MessageId => Id;

        public double id;
        public bool ready;

        public ExchangeIsReadyMessage()
        {
        }
        public ExchangeIsReadyMessage(double id, bool ready)
        {
            this.id = id;
            this.ready = ready;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < -9007199254740992 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
            writer.WriteBoolean((bool)ready);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (double)reader.ReadDouble();
            if (id < -9007199254740992 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of ExchangeIsReadyMessage.id.");
            }

            ready = (bool)reader.ReadBoolean();
        }

    }
}


