using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MountSetXpRatioRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1572;
        public override ushort MessageId => Id;

        public byte xpRatio;

        public MountSetXpRatioRequestMessage()
        {
        }
        public MountSetXpRatioRequestMessage(byte xpRatio)
        {
            this.xpRatio = xpRatio;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (xpRatio < 0)
            {
                throw new System.Exception("Forbidden value (" + xpRatio + ") on element xpRatio.");
            }

            writer.WriteByte((byte)xpRatio);
        }
        public override void Deserialize(IDataReader reader)
        {
            xpRatio = (byte)reader.ReadByte();
            if (xpRatio < 0)
            {
                throw new System.Exception("Forbidden value (" + xpRatio + ") on element of MountSetXpRatioRequestMessage.xpRatio.");
            }

        }


    }
}








