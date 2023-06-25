using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AllianceTaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionExtendedMessage  
    { 
        public new const ushort Id = 5982;
        public override ushort MessageId => Id;

        public BasicNamedAllianceInformations alliance;

        public AllianceTaxCollectorDialogQuestionExtendedMessage()
        {
        }
        public AllianceTaxCollectorDialogQuestionExtendedMessage(BasicNamedAllianceInformations alliance,BasicGuildInformations guildInfo,short maxPods,short prospecting,short wisdom,byte taxCollectorsCount,int taxCollectorAttack,long kamas,long experience,int pods,long itemsValue)
        {
            this.alliance = alliance;
            this.guildInfo = guildInfo;
            this.maxPods = maxPods;
            this.prospecting = prospecting;
            this.wisdom = wisdom;
            this.taxCollectorsCount = taxCollectorsCount;
            this.taxCollectorAttack = taxCollectorAttack;
            this.kamas = kamas;
            this.experience = experience;
            this.pods = pods;
            this.itemsValue = itemsValue;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            alliance.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            alliance = new BasicNamedAllianceInformations();
            alliance.Deserialize(reader);
        }


    }
}








