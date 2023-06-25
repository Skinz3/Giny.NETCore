using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceModificationStartedMessage : NetworkMessage  
    { 
        public  const ushort Id = 8279;
        public override ushort MessageId => Id;

        public bool canChangeName;
        public bool canChangeTag;
        public bool canChangeEmblem;

        public AllianceModificationStartedMessage()
        {
        }
        public AllianceModificationStartedMessage(bool canChangeName,bool canChangeTag,bool canChangeEmblem)
        {
            this.canChangeName = canChangeName;
            this.canChangeTag = canChangeTag;
            this.canChangeEmblem = canChangeEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,canChangeName);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,canChangeTag);
            _box0 = BooleanByteWrapper.SetFlag(_box0,2,canChangeEmblem);
            writer.WriteByte((byte)_box0);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            canChangeName = BooleanByteWrapper.GetFlag(_box0,0);
            canChangeTag = BooleanByteWrapper.GetFlag(_box0,1);
            canChangeEmblem = BooleanByteWrapper.GetFlag(_box0,2);
        }


    }
}








