using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{
    public class MountInformationRequestMessage : NetworkMessage
    {
        public const ushort Id = 4256;
        public override ushort MessageId => Id;

        public double id;
        public double time;

        public MountInformationRequestMessage()
        {
        }
        public MountInformationRequestMessage(double id, double time)
        {
            this.id = id;
            this.time = time;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (id < -9007199254740992 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element id.");
            }

            writer.WriteDouble((double)id);
            if (time < -9007199254740992 || time > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + time + ") on element time.");
            }

            writer.WriteDouble((double)time);
        }
        public override void Deserialize(IDataReader reader)
        {
            id = (double)reader.ReadDouble();
            if (id < -9007199254740992 || id > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + id + ") on element of MountInformationRequestMessage.id.");
            }

            time = (double)reader.ReadDouble();
            if (time < -9007199254740992 || time > 9007199254740992)
            {
                throw new System.Exception("Forbidden value (" + time + ") on element of MountInformationRequestMessage.time.");
            }

        }

    }
}


