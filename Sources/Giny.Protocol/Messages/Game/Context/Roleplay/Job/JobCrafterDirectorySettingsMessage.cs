using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobCrafterDirectorySettingsMessage : NetworkMessage  
    { 
        public  const ushort Id = 7632;
        public override ushort MessageId => Id;

        public JobCrafterDirectorySettings[] craftersSettings;

        public JobCrafterDirectorySettingsMessage()
        {
        }
        public JobCrafterDirectorySettingsMessage(JobCrafterDirectorySettings[] craftersSettings)
        {
            this.craftersSettings = craftersSettings;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)craftersSettings.Length);
            for (uint _i1 = 0;_i1 < craftersSettings.Length;_i1++)
            {
                (craftersSettings[_i1] as JobCrafterDirectorySettings).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            JobCrafterDirectorySettings _item1 = null;
            uint _craftersSettingsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _craftersSettingsLen;_i1++)
            {
                _item1 = new JobCrafterDirectorySettings();
                _item1.Deserialize(reader);
                craftersSettings[_i1] = _item1;
            }

        }


    }
}








