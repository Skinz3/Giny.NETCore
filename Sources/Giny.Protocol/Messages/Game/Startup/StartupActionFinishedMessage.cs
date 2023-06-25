using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class StartupActionFinishedMessage : NetworkMessage  
    { 
        public  const ushort Id = 193;
        public override ushort MessageId => Id;

        public bool success;
        public int actionId;
        public bool automaticAction;

        public StartupActionFinishedMessage()
        {
        }
        public StartupActionFinishedMessage(bool success,int actionId,bool automaticAction)
        {
            this.success = success;
            this.actionId = actionId;
            this.automaticAction = automaticAction;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,success);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,automaticAction);
            writer.WriteByte((byte)_box0);
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element actionId.");
            }

            writer.WriteInt((int)actionId);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            success = BooleanByteWrapper.GetFlag(_box0,0);
            automaticAction = BooleanByteWrapper.GetFlag(_box0,1);
            actionId = (int)reader.ReadInt();
            if (actionId < 0)
            {
                throw new System.Exception("Forbidden value (" + actionId + ") on element of StartupActionFinishedMessage.actionId.");
            }

        }


    }
}








