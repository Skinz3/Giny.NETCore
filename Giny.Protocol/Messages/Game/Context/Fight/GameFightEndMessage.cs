using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightEndMessage : NetworkMessage  
    { 
        public  const ushort Id = 1798;
        public override ushort MessageId => Id;

        public int duration;
        public short rewardRate;
        public short lootShareLimitMalus;
        public FightResultListEntry[] results;
        public NamedPartyTeamWithOutcome[] namedPartyTeamsOutcomes;

        public GameFightEndMessage()
        {
        }
        public GameFightEndMessage(int duration,short rewardRate,short lootShareLimitMalus,FightResultListEntry[] results,NamedPartyTeamWithOutcome[] namedPartyTeamsOutcomes)
        {
            this.duration = duration;
            this.rewardRate = rewardRate;
            this.lootShareLimitMalus = lootShareLimitMalus;
            this.results = results;
            this.namedPartyTeamsOutcomes = namedPartyTeamsOutcomes;
        }
        public override void Serialize(IDataWriter writer)
        {
            if (duration < 0)
            {
                throw new System.Exception("Forbidden value (" + duration + ") on element duration.");
            }

            writer.WriteInt((int)duration);
            writer.WriteVarShort((short)rewardRate);
            writer.WriteShort((short)lootShareLimitMalus);
            writer.WriteShort((short)results.Length);
            for (uint _i4 = 0;_i4 < results.Length;_i4++)
            {
                writer.WriteShort((short)(results[_i4] as FightResultListEntry).TypeId);
                (results[_i4] as FightResultListEntry).Serialize(writer);
            }

            writer.WriteShort((short)namedPartyTeamsOutcomes.Length);
            for (uint _i5 = 0;_i5 < namedPartyTeamsOutcomes.Length;_i5++)
            {
                (namedPartyTeamsOutcomes[_i5] as NamedPartyTeamWithOutcome).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            uint _id4 = 0;
            FightResultListEntry _item4 = null;
            NamedPartyTeamWithOutcome _item5 = null;
            duration = (int)reader.ReadInt();
            if (duration < 0)
            {
                throw new System.Exception("Forbidden value (" + duration + ") on element of GameFightEndMessage.duration.");
            }

            rewardRate = (short)reader.ReadVarShort();
            lootShareLimitMalus = (short)reader.ReadShort();
            uint _resultsLen = (uint)reader.ReadUShort();
            for (uint _i4 = 0;_i4 < _resultsLen;_i4++)
            {
                _id4 = (uint)reader.ReadUShort();
                _item4 = ProtocolTypeManager.GetInstance<FightResultListEntry>((short)_id4);
                _item4.Deserialize(reader);
                results[_i4] = _item4;
            }

            uint _namedPartyTeamsOutcomesLen = (uint)reader.ReadUShort();
            for (uint _i5 = 0;_i5 < _namedPartyTeamsOutcomesLen;_i5++)
            {
                _item5 = new NamedPartyTeamWithOutcome();
                _item5.Deserialize(reader);
                namedPartyTeamsOutcomes[_i5] = _item5;
            }

        }


    }
}








