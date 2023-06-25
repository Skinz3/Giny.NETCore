using System.Collections.Generic;
using Giny.Core.Network.Messages;
using Giny.Protocol.Types;
using Giny.Core.IO.Interfaces;
using Giny.Protocol;
using Giny.Protocol.Enums;

namespace Giny.Protocol.Messages
{ 
    public class GameFightResumeMessage : GameFightSpectateMessage  
    { 
        public new const ushort Id = 6953;
        public override ushort MessageId => Id;

        public GameFightSpellCooldown[] spellCooldowns;
        public byte summonCount;
        public byte bombCount;

        public GameFightResumeMessage()
        {
        }
        public GameFightResumeMessage(GameFightSpellCooldown[] spellCooldowns,byte summonCount,byte bombCount,FightDispellableEffectExtendedInformations[] effects,GameActionMark[] marks,short gameTurn,int fightStart,Idol[] idols,GameFightEffectTriggerCount[] fxTriggerCounts)
        {
            this.spellCooldowns = spellCooldowns;
            this.summonCount = summonCount;
            this.bombCount = bombCount;
            this.effects = effects;
            this.marks = marks;
            this.gameTurn = gameTurn;
            this.fightStart = fightStart;
            this.idols = idols;
            this.fxTriggerCounts = fxTriggerCounts;
        }
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)spellCooldowns.Length);
            for (uint _i1 = 0;_i1 < spellCooldowns.Length;_i1++)
            {
                (spellCooldowns[_i1] as GameFightSpellCooldown).Serialize(writer);
            }

            if (summonCount < 0)
            {
                throw new System.Exception("Forbidden value (" + summonCount + ") on element summonCount.");
            }

            writer.WriteByte((byte)summonCount);
            if (bombCount < 0)
            {
                throw new System.Exception("Forbidden value (" + bombCount + ") on element bombCount.");
            }

            writer.WriteByte((byte)bombCount);
        }
        public override void Deserialize(IDataReader reader)
        {
            GameFightSpellCooldown _item1 = null;
            base.Deserialize(reader);
            uint _spellCooldownsLen = (uint)reader.ReadUShort();
            for (uint _i1 = 0;_i1 < _spellCooldownsLen;_i1++)
            {
                _item1 = new GameFightSpellCooldown();
                _item1.Deserialize(reader);
                spellCooldowns[_i1] = _item1;
            }

            summonCount = (byte)reader.ReadByte();
            if (summonCount < 0)
            {
                throw new System.Exception("Forbidden value (" + summonCount + ") on element of GameFightResumeMessage.summonCount.");
            }

            bombCount = (byte)reader.ReadByte();
            if (bombCount < 0)
            {
                throw new System.Exception("Forbidden value (" + bombCount + ") on element of GameFightResumeMessage.bombCount.");
            }

        }


    }
}








