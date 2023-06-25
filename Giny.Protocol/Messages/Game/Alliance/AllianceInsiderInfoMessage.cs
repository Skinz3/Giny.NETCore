using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceInsiderInfoMessage : NetworkMessage  
    { 
        public  const ushort Id = 1950;
        public override ushort MessageId => Id;

        public AllianceFactSheetInformations allianceInfos;
        public GuildInsiderFactSheetInformations[] guilds;
        public PrismSubareaEmptyInfo[] prisms;

        public AllianceInsiderInfoMessage()
        {
        }
        public AllianceInsiderInfoMessage(AllianceFactSheetInformations allianceInfos,GuildInsiderFactSheetInformations[] guilds,PrismSubareaEmptyInfo[] prisms)
        {
            this.allianceInfos = allianceInfos;
            this.guilds = guilds;
            this.prisms = prisms;
        }
        public override void Serialize(IDataWriter writer)
        {
            allianceInfos.Serialize(writer);
            writer.WriteShort((short)guilds.Length);
            for (uint _i2 = 0;_i2 < guilds.Length;_i2++)
            {
                (guilds[_i2] as GuildInsiderFactSheetInformations).Serialize(writer);
            }

            writer.WriteShort((short)prisms.Length);
            for (uint _i3 = 0;_i3 < prisms.Length;_i3++)
            {
                writer.WriteShort((short)(prisms[_i3] as PrismSubareaEmptyInfo).TypeId);
                (prisms[_i3] as PrismSubareaEmptyInfo).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            GuildInsiderFactSheetInformations _item2 = null;
            uint _id3 = 0;
            PrismSubareaEmptyInfo _item3 = null;
            allianceInfos = new AllianceFactSheetInformations();
            allianceInfos.Deserialize(reader);
            uint _guildsLen = (uint)reader.ReadUShort();
            for (uint _i2 = 0;_i2 < _guildsLen;_i2++)
            {
                _item2 = new GuildInsiderFactSheetInformations();
                _item2.Deserialize(reader);
                guilds[_i2] = _item2;
            }

            uint _prismsLen = (uint)reader.ReadUShort();
            for (uint _i3 = 0;_i3 < _prismsLen;_i3++)
            {
                _id3 = (uint)reader.ReadUShort();
                _item3 = ProtocolTypeManager.GetInstance<PrismSubareaEmptyInfo>((short)_id3);
                _item3.Deserialize(reader);
                prisms[_i3] = _item3;
            }

        }


    }
}








