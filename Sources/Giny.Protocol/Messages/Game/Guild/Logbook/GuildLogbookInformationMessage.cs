using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildLogbookInformationMessage : NetworkMessage  
    { 
        public  const ushort Id = 7468;
        public override ushort MessageId => Id;

        public GuildLogbookEntryBasicInformation[] globalActivities;
        public GuildLogbookEntryBasicInformation[] chestActivities;

        public GuildLogbookInformationMessage()
        {
        }
        public GuildLogbookInformationMessage(GuildLogbookEntryBasicInformation[] globalActivities,GuildLogbookEntryBasicInformation[] chestActivities)
        {
            this.globalActivities = globalActivities;
            this.chestActivities = chestActivities;
        }
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)globalActivities.Length);
            for (uint _i1 = 0;_i1 < globalActivities.Length;_i1++)
            {
                writer.WriteShort((short)(globalActivities[_i1] as GuildLogbookEntryBasicInformation).TypeId);
                (globalActivities[_i1] as GuildLogbookEntryBasicInformation).Serialize(writer);
            }

            writer.WriteShort((short)chestActivities.Length);
            for (uint _i2 = 0;_i2 < chestActivities.Length;_i2++)
            {
                writer.WriteShort((short)(chestActivities[_i2] as GuildLogbookEntryBasicInformation).TypeId);
                (chestActivities[_i2] as GuildLogbookEntryBasicInformation).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id1 = 0;
            GuildLogbookEntryBasicInformation _item1 = null;
            uint _id2 = 0;
            GuildLogbookEntryBasicInformation _item2 = null;
            uint _globalActivitiesLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _globalActivitiesLen;_i1++)
            {
                _id1 = (uint)reader.ReadUShort();
                _item1 = ProtocolTypeManager.GetInstance<GuildLogbookEntryBasicInformation>((short)_id1);
                _item1.Deserialize(reader);
                globalActivities[_i1] = _item1;
            }

            uint _chestActivitiesLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _chestActivitiesLen;_i2++)
            {
                _id2 = (uint)reader.ReadUShort();
                _item2 = ProtocolTypeManager.GetInstance<GuildLogbookEntryBasicInformation>((short)_id2);
                _item2.Deserialize(reader);
                chestActivities[_i2] = _item2;
            }

        }


    }
}








