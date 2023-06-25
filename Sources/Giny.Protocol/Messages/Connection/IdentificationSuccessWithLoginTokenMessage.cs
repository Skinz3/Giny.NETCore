using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class IdentificationSuccessWithLoginTokenMessage : IdentificationSuccessMessage  
    { 
        public new const ushort Id = 427;
        public override ushort MessageId => Id;

        public string loginToken;

        public IdentificationSuccessWithLoginTokenMessage()
        {
        }
        public IdentificationSuccessWithLoginTokenMessage(string loginToken,string login,AccountTagInformation accountTag,int accountId,byte communityId,bool hasRights,bool hasConsoleRight,string secretQuestion,double accountCreation,double subscriptionElapsedDuration,double subscriptionEndDate,bool wasAlreadyConnected,byte havenbagAvailableRoom,bool isAccountForced)
        {
            this.loginToken = loginToken;
            this.login = login;
            this.accountTag = accountTag;
            this.accountId = accountId;
            this.communityId = communityId;
            this.hasRights = hasRights;
            this.hasConsoleRight = hasConsoleRight;
            this.secretQuestion = secretQuestion;
            this.accountCreation = accountCreation;
            this.subscriptionElapsedDuration = subscriptionElapsedDuration;
            this.subscriptionEndDate = subscriptionEndDate;
            this.wasAlreadyConnected = wasAlreadyConnected;
            this.havenbagAvailableRoom = havenbagAvailableRoom;
            this.isAccountForced = isAccountForced;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)loginToken);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            loginToken = (string)reader.ReadUTF();
        }


    }
}








