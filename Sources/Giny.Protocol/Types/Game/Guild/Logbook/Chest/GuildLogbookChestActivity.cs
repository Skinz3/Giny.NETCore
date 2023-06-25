using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class GuildLogbookChestActivity : GuildLogbookEntryBasicInformation  
    { 
        public new const ushort Id = 3291;
        public override ushort TypeId => Id;

        public long playerId;
        public string playerName;
        public byte eventType;
        public int quantity;
        public ObjectItemNotInContainer @object;

        public GuildLogbookChestActivity()
        {
        }
        public GuildLogbookChestActivity(long playerId,string playerName,byte eventType,int quantity,ObjectItemNotInContainer @object,int id,double date)
        {
            this.playerId = playerId;
            this.playerName = playerName;
            this.eventType = eventType;
            this.quantity = quantity;
            this.@object = @object;
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
            writer.WriteByte((byte)eventType);
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element quantity.");
            }

            writer.WriteInt((int)quantity);
            @object.Serialize(writer);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            playerId = (long)reader.ReadVarUhLong();
            if (playerId < 0 || playerId > 9.00719925474099E+15)
            {
                throw new System.Exception("Forbidden value (" + playerId + ") on element of GuildLogbookChestActivity.playerId.");
            }

            playerName = (string)reader.ReadUTF();
            eventType = (byte)reader.ReadByte();
            if (eventType < 0)
            {
                throw new System.Exception("Forbidden value (" + eventType + ") on element of GuildLogbookChestActivity.eventType.");
            }

            quantity = (int)reader.ReadInt();
            if (quantity < 0)
            {
                throw new System.Exception("Forbidden value (" + quantity + ") on element of GuildLogbookChestActivity.quantity.");
            }

            @object = new ObjectItemNotInContainer();
            @object.Deserialize(reader);
        }


    }
}








