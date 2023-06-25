using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildPlayerFlowActivity : GuildLogbookEntryBasicInformation  
    { 
        public new const ushort Id = 9219;
        public override ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public byte playerFlowEventType;

        public GuildPlayerFlowActivity()
        {
        }
        public GuildPlayerFlowActivity(long playerId,string playerName,byte playerFlowEventType,int id,double date)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.playerFlowEventType = playerFlowEventType;
            this.id = id;
            this.date = date;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element playerId.");
            }

            writer.WriteVarLong((long)playerId);
            writer.WriteUTF((string)playerName);
            writer.WriteByte((byte)playerFlowEventType);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GuildPlayerFlowActivity.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            playerFlowEventType = (byte)reader.ReadByte();
            if (playerFlowEventType < 0)
            {
                throw new System.Exception("Forbidden value (" + playerFlowEventType + ") on element of GuildPlayerFlowActivity.playerFlowEventType.");
            }

        }


    }
}








