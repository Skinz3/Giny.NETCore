using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class ShortcutBarSwapRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 1940;
        public override ushort MessageId => Id;

        public byte barType;
        public byte firstSlot;
        public byte secondSlot;

        public ShortcutBarSwapRequestMessage()
        {
        }
        public ShortcutBarSwapRequestMessage(byte barType,byte firstSlot,byte secondSlot)
        {
            this.barType = barType;
            this.firstSlot = firstSlot;
            this.secondSlot = secondSlot;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)barType);
            if (firstSlot < 0 || firstSlot > 99)
            {
                throw new System.Exception("Forbidden value (" + firstSlot + ") on element firstSlot.");
            }

            writer.WriteByte((byte)firstSlot);
            if (secondSlot < 0 || secondSlot > 99)
            {
                throw new System.Exception("Forbidden value (" + secondSlot + ") on element secondSlot.");
            }

            writer.WriteByte((byte)secondSlot);
        }
        public override void Deserialize(IDataReader reader)
        {
            barType = (byte)reader.ReadByte();
            if (barType < 0)
            {
                throw new System.Exception("Forbidden value (" + barType + ") on element of ShortcutBarSwapRequestMessage.barType.");
            }

            firstSlot = (byte)reader.ReadByte();
            if (firstSlot < 0 || firstSlot > 99)
            {
                throw new System.Exception("Forbidden value (" + firstSlot + ") on element of ShortcutBarSwapRequestMessage.firstSlot.");
            }

            secondSlot = (byte)reader.ReadByte();
            if (secondSlot < 0 || secondSlot > 99)
            {
                throw new System.Exception("Forbidden value (" + secondSlot + ") on element of ShortcutBarSwapRequestMessage.secondSlot.");
            }

        }


    }
}








