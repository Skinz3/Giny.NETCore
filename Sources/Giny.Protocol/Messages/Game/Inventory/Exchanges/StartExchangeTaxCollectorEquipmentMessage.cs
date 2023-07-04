using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class StartExchangeTaxCollectorEquipmentMessage : NetworkMessage
    {
        public const ushort Id = 740;
        public override ushort MessageId => Id;

        public double uid;

        public StartExchangeTaxCollectorEquipmentMessage()
        {
        }
        public StartExchangeTaxCollectorEquipmentMessage(double uid)
        {
            this.uid = uid;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (uid < 0 || uid > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element uid.");
            }

            writer.WriteDouble((double)uid);
        }
        public override void Deserialize(IDataReader reader)
        {
            uid = (double)reader.ReadDouble();
            if (uid < 0 || uid > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + uid + ") on element of StartExchangeTaxCollectorEquipmentMessage.uid.");
            }

        }

    }
}


