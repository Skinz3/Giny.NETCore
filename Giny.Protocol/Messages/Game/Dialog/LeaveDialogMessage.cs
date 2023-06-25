using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LeaveDialogMessage : NetworkMessage  
    { 
        public  const ushort Id = 5131;
        public override ushort MessageId => Id;

        public byte dialogType;

        public LeaveDialogMessage()
        {
        }
        public LeaveDialogMessage(byte dialogType)
        {
            this.dialogType = dialogType;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte((byte)dialogType);
        }
        public override void Deserialize(IDataReader reader)
        {
            dialogType = (byte)reader.ReadByte();
            if (dialogType < 0)
            {
                throw new System.Exception("Forbidden value (" + dialogType + ") on element of LeaveDialogMessage.dialogType.");
            }

        }


    }
}








