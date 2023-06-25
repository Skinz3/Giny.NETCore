using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GuildInAllianceFactsMessage : GuildFactsMessage  
    { 
        public new const ushort Id = 3808;
        public override ushort MessageId => Id;

        public BasicNamedAllianceInformations allianceInfos;

        public GuildInAllianceFactsMessage()
        {
        }
        public GuildInAllianceFactsMessage(BasicNamedAllianceInformations allianceInfos,GuildFactSheetInformations infos,int creationDate,short nbTaxCollectors,CharacterMinimalGuildPublicInformations[] members)
        {
            this.allianceInfos = allianceInfos;
            this.infos = infos;
            this.creationDate = creationDate;
            this.nbTaxCollectors = nbTaxCollectors;
            this.members = members;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            allianceInfos.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            allianceInfos = new BasicNamedAllianceInformations();
            allianceInfos.Deserialize(reader);
        }


    }
}








