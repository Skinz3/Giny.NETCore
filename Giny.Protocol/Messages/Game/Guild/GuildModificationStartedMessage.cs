using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildModificationStartedMessage : NetworkMessage  
    { 
        public  const ushort Id = 4342;
        public override ushort MessageId => Id;

        public bool canChangeName;
        public bool canChangeEmblem;

        public GuildModificationStartedMessage()
        {
        }
        public GuildModificationStartedMessage(bool canChangeName,bool canChangeEmblem)
        {
            this.canChangeName = canChangeName;
            this.canChangeEmblem = canChangeEmblem;
        }
        public override void Serialize(IDataWriter writer)
        {
            byte _box0 = 0;
            _box0 = BooleanByteWrapper.SetFlag(_box0,0,canChangeName);
            _box0 = BooleanByteWrapper.SetFlag(_box0,1,canChangeEmblem);
            writer.WriteByte((byte)_box0);
        }
        public override void Deserialize(IDataReader reader)
        {
            byte _box0 = reader.ReadByte();
            canChangeName = BooleanByteWrapper.GetFlag(_box0,0);
            canChangeEmblem = BooleanByteWrapper.GetFlag(_box0,1);
        }


    }
}








