using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class AchievementFinishedInformationMessage : AchievementFinishedMessage  
    { 
        public new const ushort Id = 7708;
        public override ushort MessageId => Id;

        public string name;
        public long playerId;

        public AchievementFinishedInformationMessage()
        {
        }
        public AchievementFinishedInformationMessage(string name,long playerId,AchievementAchievedRewardable achievement)
        {
            this.name = name;
            this.playerId = playerId;
            this.achievement = achievement;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF((string)name);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            name = (string)reader.ReadUTF();
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of AchievementFinishedInformationMessage.playerId.");
            }

        }


    }
}








