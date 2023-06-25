using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class MountHarnessColorsUpdateRequestMessage : NetworkMessage  
    { 
        public  const ushort Id = 8550;
        public override ushort MessageId => Id;

        public bool useHarnessColors;

        public MountHarnessColorsUpdateRequestMessage()
        {
        }
        public MountHarnessColorsUpdateRequestMessage(bool useHarnessColors)
        {
            this.useHarnessColors = useHarnessColors;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean((bool)useHarnessColors);
        }
        public override void Deserialize(IDataReader reader)
        {
            useHarnessColors = (bool)reader.ReadBoolean();
        }


    }
}








