using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class FocusedExchangeReadyMessage : ExchangeReadyMessage  
    { 
        public new const ushort Id = 4485;
        public override ushort MessageId => Id;

        public int focusActionId;

        public FocusedExchangeReadyMessage()
        {
        }
        public FocusedExchangeReadyMessage(int focusActionId,bool ready,short step)
        {
            this.focusActionId = focusActionId;
            this.ready = ready;
            this.step = step;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (focusActionId < 0)
            {
                throw new System.Exception("Forbidden value (" + focusActionId + ") on element focusActionId.");
            }

            writer.WriteVarInt((int)focusActionId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            focusActionId = (int)reader.ReadVarUhInt();
            if (focusActionId < 0)
            {
                throw new System.Exception("Forbidden value (" + focusActionId + ") on element of FocusedExchangeReadyMessage.focusActionId.");
            }

        }


    }
}








