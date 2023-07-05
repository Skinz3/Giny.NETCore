using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class SubareaRewardRateMessage : NetworkMessage
    {
        public const ushort Id = 5569;
        public override ushort MessageId => Id;

        public short subAreaRate;

        public SubareaRewardRateMessage()
        {
        }
        public SubareaRewardRateMessage(short subAreaRate)
        {
            this.subAreaRate = subAreaRate;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort((short)subAreaRate);
        }
        public override void Deserialize(IDataReader reader)
        {
            subAreaRate = (short)reader.ReadVarShort();
        }

    }
}


