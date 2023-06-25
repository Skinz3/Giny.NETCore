using System.Collections.Generic;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Types
{ 
    public class FightResultTaxCollectorListEntry : FightResultFighterListEntry  
    { 
        public new const ushort Id = 1668;
        public override ushort TypeId => Id;

        public byte level;
        public BasicGuildInformations guildInfo;
        public int experienceForGuild;

        public FightResultTaxCollectorListEntry()
        {
        }
        public FightResultTaxCollectorListEntry(byte level,BasicGuildInformations guildInfo,int experienceForGuild,short outcome,byte wave,FightLoot rewards,double id,bool alive)
        {
            this.level = level;
            this.guildInfo = guildInfo;
            this.experienceForGuild = experienceForGuild;
            this.outcome = outcome;
            this.wave = wave;
            this.rewards = rewards;
            this.id = id;
            this.alive = alive;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element level.");
            }

            writer.WriteByte((byte)level);
            guildInfo.Serialize(writer);
            writer.WriteInt((int)experienceForGuild);
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            level = (byte)reader.ReadSByte();
            if (level < 1 || level > 200)
            {
                throw new System.Exception("Forbidden value (" + level + ") on element of FightResultTaxCollectorListEntry.level.");
            }

            guildInfo = new BasicGuildInformations();
            guildInfo.Deserialize(reader);
            experienceForGuild = (int)reader.ReadInt();
        }


    }
}








