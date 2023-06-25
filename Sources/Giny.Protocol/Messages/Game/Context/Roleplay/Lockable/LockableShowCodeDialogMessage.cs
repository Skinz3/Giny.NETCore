using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class LockableShowCodeDialogMessage : NetworkMessage  
    { 
        public  const ushort Id = 5125;
        public override ushort MessageId => Id;

        public bool changeOrUse;
        public byte codeSize;

        public LockableShowCodeDialogMessage()
        {
        }
        public LockableShowCodeDialogMessage(bool changeOrUse,byte codeSize)
        {
            this.changeOrUse = changeOrUse;
            this.codeSize = codeSize;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)changeOrUse);
            if (codeSize < 0)
            {
                throw new System.Exception("Forbidden value (" + codeSize + ") on element codeSize.");
            }

            writer.WriteByte((byte)codeSize);
        }
        public override void Deserialize(IDataReader reader)
        {
            changeOrUse = (bool)reader.ReadBoolean();
            codeSize = (byte)reader.ReadByte();
            if (codeSize < 0)
            {
                throw new System.Exception("Forbidden value (" + codeSize + ") on element of LockableShowCodeDialogMessage.codeSize.");
            }

        }


    }
}








