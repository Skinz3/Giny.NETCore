using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightResumeWithSlavesMessage : GameFightResumeMessage  
    { 
        public new const ushort Id = 1683;
        public override ushort MessageId => Id;

        public GameFightResumeSlaveInfo[] slavesInfo;

        public GameFightResumeWithSlavesMessage()
        {
        }
        public GameFightResumeWithSlavesMessage(GameFightResumeSlaveInfo[] slavesInfo,FightDispellableEffectExtendedInformations[] effects,GameActionMark[] marks,short gameTurn,int fightStart,Idol[] idols,GameFightEffectTriggerCount[] fxTriggerCounts,GameFightSpellCooldown[] spellCooldowns,byte summonCount,byte bombCount)
        {
            this.slavesInfo = slavesInfo;
            this.effects = effects;
            this.marks = marks;
            this.gameTurn = gameTurn;
            this.fightStart = fightStart;
            this.idols = idols;
            this.fxTriggerCounts = fxTriggerCounts;
            this.spellCooldowns = spellCooldowns;
            this.summonCount = summonCount;
            this.bombCount = bombCount;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)slavesInfo.Length);
            for (uint _i1 = 0;_i1 < slavesInfo.Length;_i1++)
            {
                (slavesInfo[_i1] as GameFightResumeSlaveInfo).Serialize(writer);
            }

        }
        public override void Deserialize(IDataReader reader)
        {
            GameFightResumeSlaveInfo _item1 = null;
            base.Deserialize(reader);
            uint _slavesInfoLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _slavesInfoLen;_i1++)
            {
                _item1 = new GameFightResumeSlaveInfo();
                _item1.Deserialize(reader);
                slavesInfo[_i1] = _item1;
            }

        }


    }
}








