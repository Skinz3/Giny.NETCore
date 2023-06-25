using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class JobCrafterDirectoryEntryMessage : NetworkMessage  
    { 
        public  const ushort Id = 1870;
        public override ushort MessageId => Id;

        public JobCrafterDirectoryEntryPlayerInfo playerInfo;
        public JobCrafterDirectoryEntryJobInfo[] jobInfoList;
        public EntityLook playerLook;

        public JobCrafterDirectoryEntryMessage()
        {
        }
        public JobCrafterDirectoryEntryMessage(JobCrafterDirectoryEntryPlayerInfo playerInfo,JobCrafterDirectoryEntryJobInfo[] jobInfoList,EntityLook playerLook)
        {
            this.playerInfo = playerInfo;
            this.jobInfoList = jobInfoList;
            this.playerLook = playerLook;
        }
        public override void Serialize(IDataWriter writer)
        {
            playerInfo.Serialize(writer);
            writer.WriteShort((short)jobInfoList.Length);
            for (uint _i2 = 0;_i2 < jobInfoList.Length;_i2++)
            {
                (jobInfoList[_i2] as JobCrafterDirectoryEntryJobInfo).Serialize(writer);
            }

            playerLook.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            JobCrafterDirectoryEntryJobInfo _item2 = null;
            playerInfo = new JobCrafterDirectoryEntryPlayerInfo();
            playerInfo.Deserialize(reader);
            uint _jobInfoListLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _jobInfoListLen;_i2++)
            {
                _item2 = new JobCrafterDirectoryEntryJobInfo();
                _item2.Deserialize(reader);
                jobInfoList[_i2] = _item2;
            }

            playerLook = new EntityLook();
            playerLook.Deserialize(reader);
        }


    }
}








