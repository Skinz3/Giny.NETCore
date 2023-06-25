using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildPaddockActivity : GuildLogbookEntryBasicInformation  
    { 
        public new const ushort Id = 9842;
        public override ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public MapCoordinatesExtended paddockCoordinates;
        public double farmId;
        public byte paddockEventType;

        public GuildPaddockActivity()
        {
        }
        public GuildPaddockActivity(long playerId,string playerName,MapCoordinatesExtended paddockCoordinates,double farmId,byte paddockEventType,int id,double date)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.paddockCoordinates = paddockCoordinates;
            this.farmId = farmId;
            this.paddockEventType = paddockEventType;
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
            paddockCoordinates.Serialize(writer);
            if (farmId < 0 || farmId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + farmId + ") on element farmId.");
            }

            writer.WriteDouble((double)farmId);
            writer.WriteByte((byte)paddockEventType);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GuildPaddockActivity.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            paddockCoordinates = new MapCoordinatesExtended();
            paddockCoordinates.Deserialize(reader);
            farmId = (double)reader.ReadDouble();
            if (farmId < 0 || farmId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + farmId + ") on element of GuildPaddockActivity.farmId.");
            }

            paddockEventType = (byte)reader.ReadByte();
            if (paddockEventType < 0)
            {
                throw new System.Exception("Forbidden value (" + paddockEventType + ") on element of GuildPaddockActivity.paddockEventType.");
            }

        }


    }
}








