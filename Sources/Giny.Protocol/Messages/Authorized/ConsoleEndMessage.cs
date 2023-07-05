using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class ConsoleEndMessage : NetworkMessage
    {
        public const ushort Id = 7083;
        public override ushort MessageId => Id;

        public Uuid consoleUuid;
        public bool isSuccess;

        public ConsoleEndMessage()
        {
        }
        public ConsoleEndMessage(Uuid consoleUuid, bool isSuccess)
        {
            this.consoleUuid = consoleUuid;
            this.isSuccess = isSuccess;
        }
        public override void Serialize(IDataWriter writer)
        {
            consoleUuid.Serialize(writer);
            writer.WriteBoolean((bool)isSuccess);
        }
        public override void Deserialize(IDataReader reader)
        {
            consoleUuid = new Uuid();
            consoleUuid.Deserialize(reader);
            isSuccess = (bool)reader.ReadBoolean();
        }

    }
}


